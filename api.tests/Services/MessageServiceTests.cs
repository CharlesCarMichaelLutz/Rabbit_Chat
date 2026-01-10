using api.Models.Message;
using api.Repositories;
using api.Services;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.tests.Services
{
    public class MessageServiceTests
    {
        private readonly MessageService _messageService;
        private readonly IMessageRepository _messageRepository;
        public MessageServiceTests()
        {
            _messageRepository = A.Fake<IMessageRepository>();
            _messageService = new MessageService(_messageRepository);
        }

    }
}
