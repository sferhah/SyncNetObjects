using Ferhah.SyncNetObjects.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{

    public class Syncer<T> where T : class, new()
    {

        protected T originalSet;
        protected T newestSet;

        protected IndexedCollectionSet<T> originalIndexedCollectionSet;
        protected IndexedCollectionSet<T> newestIndexedCollectionSet;
        protected SyncComparisonResult<T> comparisonResult;

        public SyncConfigSet<T> SyncConfigSet { get; private set; }

        public Syncer(T originalSet, T newestSet, SyncConfigSet<T> syncConfigSet)            
        {
            SyncConfigSet = syncConfigSet;

            this.originalSet = originalSet;
            this.newestSet = newestSet;

            IndexBoth();
        }

        public virtual void SyncSet()
        {   

            if (this.comparisonResult == null)
            {
                CompareSet();
            }

            UpdateReferences(comparisonResult.DeltaAddedIndexedCollectionSet,
                comparisonResult.DeltaUpdateIndexedCollectionSet);

            Remove(comparisonResult.DeltaRemoveIndexedCollectionSet);

            Add(comparisonResult.DeltaAddedIndexedCollectionSet);

            Update(comparisonResult.DeltaUpdateIndexedCollectionSet);

        }

        public virtual void IndexBoth()
        {
            this.originalIndexedCollectionSet = LinkInternalReferences(originalSet);
            this.newestIndexedCollectionSet = LinkInternalReferences(newestSet);
        }

        #region indexation
        public virtual IndexedCollectionSet<T> LinkInternalReferences(T objectSet)
        {

            IndexedCollectionSet<T> indexedCollectionSet = this.SyncConfigSet.IndexColections(objectSet);

            foreach (var indexedCollection in indexedCollectionSet.IndexedCollections)
            {
                var config = indexedCollection.Configuration;

                foreach (var item in indexedCollection.Collection)
                {
                    if (config.AllNavigationProperties.Any())
                    {
                        LinkInternalReferencesForItem(item, config.AllNavigationProperties, indexedCollectionSet);
                    }

                    foreach (var complexType in config.ComplexTypes)
                    {
                        Object child = complexType.GetValueFor(item);

                        if (child == null)
                        {
                            continue;
                        }

                        var childReferences = complexType.Configuration.AllNavigationProperties;

                        LinkInternalReferencesForItem(child, childReferences, indexedCollectionSet);
                    }

                }
            }

            return indexedCollectionSet;

        }

        public virtual void LinkInternalReferencesForItem(object item,
                                                        IEnumerable<SyncProperty> references,
                                                        IndexedCollectionSet<T> indexedCollectionSet)
        {

            foreach (var reference in references)
            {
                if (reference.Configuration == null)
                {
                    continue;
                }

                Object referenceValue = reference.GetValueFor(item);

                if (referenceValue == null)
                {
                    continue;
                }

                var referencedIndexedCollection = indexedCollectionSet[reference.Configuration];

                String key = reference.Configuration.GetKeyAsStringValue(referenceValue);

                Object referencedObject = referencedIndexedCollection[reference.Configuration.GetKeyAsStringValue(referenceValue)];

                if (referencedObject == null)
                {
                    throw new FakeReferencedObjectException(item.GetType().Name,
                                                            reference.PropertyInfo.DeclaringType.Name,
                                                            referenceValue.ToString());
                }

                reference.SetValueFor(item, referencedObject);

            }
        }
        # endregion

        #region comparison
        public virtual SyncComparisonResult<T> CompareSet()
        {
            if (this.originalIndexedCollectionSet == null)
            {
                IndexBoth();
            }

            comparisonResult = new SyncComparisonResult<T>
            {
                DeltaAddedIndexedCollectionSet = this.SyncConfigSet.IndexColections(new T()),                
                DeltaUpdateIndexedCollectionSet = this.SyncConfigSet.IndexColections(new T()),                
                DeltaRemoveIndexedCollectionSet = this.SyncConfigSet.IndexColections(new T()),                
            };

            foreach (var config in this.SyncConfigSet.Configurations)
            {
                IndexedCollection newestIndexedCollection = this.newestIndexedCollectionSet[config];
                IndexedCollection originalIndexedCollection = this.originalIndexedCollectionSet[config];

                IndexedCollection deltaAddIndexedCollection = comparisonResult.DeltaAddedIndexedCollectionSet[config];
                IndexedCollection deltaUpdateIndexedCollection = comparisonResult.DeltaUpdateIndexedCollectionSet[config];
                IndexedCollection deltaRemoveIndexedCollection = comparisonResult.DeltaRemoveIndexedCollectionSet[config];           

                foreach (var newest_kv in newestIndexedCollection)
                {
                    Object newest = newest_kv.Value;
                    String key = newest_kv.Key;                    

                    Object original = originalIndexedCollection[key];

                    if (original == null)
                    {                       
                        deltaAddIndexedCollection.Add(key, newest);
                    }
                    else if (config.RequiresUpdate(original, newest))
                    {                     
                        deltaUpdateIndexedCollection.Add(key, newest);
                    }
                }

                foreach (var original_kv in originalIndexedCollection)
                {
                    Object original = original_kv.Value;
                    String key = original_kv.Key;                 

                    Object newest = newestIndexedCollection[key];

                    if (newest == null)
                    {                        
                        deltaRemoveIndexedCollection.Add(key, original);
                    }
                }

            }

            return comparisonResult;


        }      
        #endregion
        
        #region update_ref
        public virtual void UpdateReferences(IndexedCollectionSet<T> deltaAddIndexedCollection,
                                            IndexedCollectionSet<T> DeltaUpdateIndexedCollectionSet)
        {
        
            foreach (var config in this.SyncConfigSet.Configurations.Where(x=>x.AllNavigationProperties.Any()))
            {
                foreach (var item in deltaAddIndexedCollection[config].Collection)
                {
                    foreach (var reference in config.AllNavigationProperties)
                    {
                        UpdateReference(item, reference, deltaAddIndexedCollection);
                    }


                    foreach (var complexType in config.ComplexTypes)
                    {
                        Object child = complexType.GetValueFor(item);

                        if (child == null)
                        {
                            continue;
                        }

                        foreach (var reference in complexType.Configuration.AllNavigationProperties)
                        {
                            UpdateReference(child, reference, deltaAddIndexedCollection);
                        }
                    } 
                }

                foreach (var item in DeltaUpdateIndexedCollectionSet[config].Collection)
                {
                    foreach (var reference in config.NavigationProperties)
                    {
                        UpdateReference(item, reference, deltaAddIndexedCollection);
                    }
                 
                    foreach (var complexType in config.ComplexTypes)
                    {
                        Object child = complexType.GetValueFor(item);

                        if (child == null)
                        {
                            continue;
                        }


                        foreach (var reference in complexType.Configuration.NavigationProperties)
                        {
                            UpdateReference(child, reference, deltaAddIndexedCollection);
                        }

                    }
                }

            }
        }
       

        public virtual void UpdateReference(Object o,
                                            SyncProperty syncProperty,
                                            IndexedCollectionSet<T> deltaAddIndexedCollection)
        {
            var config = syncProperty.Configuration;

            if (config == null)
            {                
                return;
            }           

            Object propertyValue = syncProperty.GetValueFor(o);

            if (propertyValue == null)
            { 
                return;
            }

            String key = config.GetKeyAsStringValue(propertyValue);
            
            Object existing_element = deltaAddIndexedCollection[config][key];            

            if (existing_element == null)
            {
                Object element = originalIndexedCollectionSet[config][key];

                if (element == null)
                { 
                    throw new Exception();
                }

                syncProperty.SetValueFor(o, element);
            }
        }
        #endregion
        
        #region remove
        public virtual void Remove(IndexedCollectionSet<T> delta)
        {
            foreach (SyncConfiguration config in this.SyncConfigSet.Configurations)
            {
                IndexedCollection originalIndexedCollection = originalIndexedCollectionSet[config];
                IndexedCollection deltaIndexedCollection = delta[config];

                foreach (var kv in deltaIndexedCollection)
                {
                    originalIndexedCollection.Remove(kv.Key, kv.Value);
                }
            }
        }
        #endregion

        #region add
        public virtual void Add(IndexedCollectionSet<T> delta)
        {
            foreach (SyncConfiguration config in this.SyncConfigSet.Configurations)
            {
                IndexedCollection originalIndexedCollection = originalIndexedCollectionSet[config];
                IndexedCollection deltaIndexedCollection = delta[config];

                foreach (var kv in deltaIndexedCollection)
                {
                    originalIndexedCollection.Add(kv.Key, kv.Value);
                }
            }
        }
        #endregion

        #region update
        public virtual void Update(IndexedCollectionSet<T> delta)
        {
            foreach (SyncConfiguration config in this.SyncConfigSet.Configurations)
            {   
                var originalIndexedCollection = originalIndexedCollectionSet[config];            

                IndexedCollection deltaIndexedCollection = delta[config];

                foreach (var kv in deltaIndexedCollection)
                {
                    Object original = originalIndexedCollection[kv.Key];

                    foreach (var updatableProperty in config.UpdatableProperties)
                    {
                        Object newest_value = updatableProperty.GetValueFor(kv.Value);
                        updatableProperty.SetValueFor(original, newest_value);
                    }
                    
                    foreach (var complexType in config.ComplexTypes)
                    {
                        Object child_original = complexType.GetValueFor(original);
                        Object child_newest = complexType.GetValueFor(kv.Value);

                        foreach (var child_updatableProperty in complexType.Configuration.UpdatableProperties)
                        {
                            Object child_newest_value = child_updatableProperty.GetValueFor(child_newest);
                            child_updatableProperty.SetValueFor(child_original, child_newest_value);
                        }
                        
                    }
                }

            }
        }
        #endregion


    }

}
