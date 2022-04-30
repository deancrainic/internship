﻿using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private IUnitOfWork _uow;

        public UpdateUserCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!_uow.UserRepository.CheckEmail(request.Email))
                throw new InvalidEmailException("Email already exists");

            if (!_uow.UserRepository.CheckPassword(request.Password))
                throw new InvalidPasswordException("Password requirements aren't met");

            if (_uow.UserRepository.GetByIdNoTracking(request.UserId) == null)
                throw new IdNotExistentException("User ID doesn't exist");

            var updatedUser = new User
            {
                UserId = request.UserId,
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _uow.UserRepository.Update(updatedUser);
            await _uow.SaveAsync();

            return updatedUser;
        }
    }
}
