using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemModel.ViewModels
{
    public interface ICloseWindow
    {
        public interface ICloseWindows
        {
            Action Close { get; set; }
            bool CanClose();
        }
    }
}
