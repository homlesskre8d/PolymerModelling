using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChemModel.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ChemModel.Messeges
{
    public class ChangeDbMEssage : ValueChangedMessage<NewMat>
    {
        public ChangeDbMEssage(NewMat message) : base(message)
        {
        }

      
    }
}
