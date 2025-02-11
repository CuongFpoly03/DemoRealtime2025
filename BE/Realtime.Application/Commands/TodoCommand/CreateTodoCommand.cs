using AutoMapper;
using MediatR;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Application.Commands.TodoCommand
{
    public class CreateTodoCommand : IRequest<Todo>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsExpired { get; set; } = false;
        public DateTime? CreatedBy { get; set; }
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        public CreateTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }
        public async Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Todo>(request);
            return await _todoRepository.AddTodoAsync(result, cancellationToken);
        }
    }
}