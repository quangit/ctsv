using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductItemsBO
    {
        private DSAContext dsaContext;
        public ConductItemsBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        #region Conduct items
        public List<ConductItem> GetListConductItems(int idConductForm)
        {
            return dsaContext.ConductItems.Where(c => c.IdConductForm.Equals(idConductForm)).OrderBy(c => c.ItemIndex).ToList();
        }

        #endregion

        public int Add(int idConductForm, string itemIndex, string itemName, int maxPoints, int idConductItemGroup)
        {
            try
            {
                ConductItem conductItem = new ConductItem(idConductForm, itemIndex, itemName, maxPoints, idConductItemGroup);
                dsaContext.ConductItems.Add(conductItem);
                dsaContext.SaveChanges();
                return conductItem.IdConductItems;
            }
            catch
            {
                return -1;
            }
        }

        public bool UpdateConductItem(int idConductItems, int idConductForm, string itemIndex, string itemName, int maxPoints, int idConductItemGroup)
        {
            try
            {
                ConductItem conductItem = dsaContext.ConductItems.SingleOrDefault(c => c.IdConductItems == idConductItems);
                if (conductItem != null)
                {
                    conductItem.IdConductForm = idConductForm;
                    conductItem.ItemIndex = itemIndex;
                    conductItem.ItemName = itemName;
                    conductItem.MaxPoints = maxPoints;
                    conductItem.IdConductItemGroup = idConductItemGroup;
                    dsaContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                ConductItem conductItem = dsaContext.ConductItems.SingleOrDefault(i => i.IdConductItems == id);
                if (conductItem != null)
                {
                    dsaContext.ConductItems.Remove(conductItem);
                    dsaContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}