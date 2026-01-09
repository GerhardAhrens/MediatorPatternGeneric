/*
 * <copyright file="MediatorGeneric.cs" company="Lifeprojects.de">
 *     Class: MediatorGeneric
 *     Copyright © Lifeprojects.de 2026
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>developer@lifeprojects.de</email>
 * <date>09.01.2026 10:31:48</date>
 * <Project>CurrentProject</Project>
 *
 * <summary>
 * Alle notwendigen Klassen und Interfaces für das generische Mediator Pattern.
 * </summary>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.You should have received a copy of the GNU General Public License along with this program. 
 * If not, see <http://www.gnu.org/licenses/>.
*/

namespace MediatorPatternGeneric.Pattern
{
    using System;
    using System.Collections.Generic;

    public interface IRequest<TResponse>
    {
    }

    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }

    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, object> _handlers = new();

        // Registrierung
        public void RegisterHandler<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
        {
            _handlers[typeof(TRequest)] = handler;
        }

        // Senden
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handler = _handlers[request.GetType()];

            return ((dynamic)handler).Handle((dynamic)request);
        }
    }
}
