using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Entities
{
    public class TextItem : BaseItem
    {
        public string SubTitle { 
            get => _subtitle; 
            set => SetProperty<string>(ref _subtitle, value); 
        }
        private string _subtitle;

        public string Detail { 
            get => _detail; 
            set => SetProperty(ref _detail, value); 
        }
        private string _detail;
    }
}
