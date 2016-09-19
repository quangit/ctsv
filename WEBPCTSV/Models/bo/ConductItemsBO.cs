namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ConductItemsBO
    {
        private readonly DSAContext dsaContext;

        public ConductItemsBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int Add(int idConductForm, string itemIndex, string itemName, int maxPoints, int idConductItemGroup)
        {
            try
            {
                ConductItem conductItem = new ConductItem(
                    idConductForm,
                    itemIndex,
                    itemName,
                    maxPoints,
                    idConductItemGroup);
                this.dsaContext.ConductItems.Add(conductItem);
                this.dsaContext.SaveChanges();
                return conductItem.IdConductItems;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                ConductItem conductItem = this.dsaContext.ConductItems.SingleOrDefault(i => i.IdConductItems == id);
                if (conductItem != null)
                {
                    this.dsaContext.ConductItems.Remove(conductItem);
                    this.dsaContext.SaveChanges();
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

        public List<ConductItem> GetListConductItems(int idConductForm)
        {
            return
                this.dsaContext.ConductItems.Where(c => c.IdConductForm.Equals(idConductForm))
                    .OrderBy(c => c.ItemIndex)
                    .ToList();
        }

        public bool UpdateConductItem(
            int idConductItems,
            int idConductForm,
            string itemIndex,
            string itemName,
            int maxPoints,
            int idConductItemGroup)
        {
            try
            {
                ConductItem conductItem =
                    this.dsaContext.ConductItems.SingleOrDefault(c => c.IdConductItems == idConductItems);
                if (conductItem != null)
                {
                    conductItem.IdConductForm = idConductForm;
                    conductItem.ItemIndex = itemIndex;
                    conductItem.ItemName = itemName;
                    conductItem.MaxPoints = maxPoints;
                    conductItem.IdConductItemGroup = idConductItemGroup;
                    this.dsaContext.SaveChanges();
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