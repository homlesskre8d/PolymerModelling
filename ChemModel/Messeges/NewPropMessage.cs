using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChemModel.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ChemModel.Messeges
{
    public class NewPropMessage : ValueChangedMessage<NewUser>
    {
        public NewPropMessage(NewUser message) : base(message) { }
    }
}
