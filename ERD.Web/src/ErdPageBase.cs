using Business.Data.Objects.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERD.Web
{
    public class ErdPageBase: PageModel, IDisposable
    {

        private BusinessSlot mSlot;
        public BusinessSlot Slot {
            get
            {
                if (this.mSlot is null)
                {
                    this.mSlot = new BusinessSlot(ErdContext.WebApp.Configuration["Database:Provider"], ErdContext.WebApp.Configuration["Database:ConnectionString"]);
                }

                return this.mSlot;
            }
        }

        public void Dispose()
        {
            this.mSlot?.Dispose();
        }
    }
}
