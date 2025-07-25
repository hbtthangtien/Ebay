using Application.DTOs.Chatbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IMessageService
    {
        public void SaveMessage(MessageDto dto);
    }
}
