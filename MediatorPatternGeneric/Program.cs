//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2026
// </copyright>
// <Template>
// 	Version 3.0.2026.1, 08.1.2026
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>08.01.2026 19:23:23</date>
//
// <summary>
// Beispiel zur Verwendung des Mediator Pattern mit Generics
// </summary>
//-----------------------------------------------------------------------

/* Imports from NET Framework */
using System;

using MediatorPatternGeneric.Pattern;

namespace MediatorPatternGeneric
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ConsoleMenu.Add("1", "Auswahl Menüpunkt 1", () => MenuPoint1());
            ConsoleMenu.Add("X", "Beenden", () => ApplicationExit());

            do
            {
                _ = ConsoleMenu.SelectKey(2, 2);
            }
            while (true);
        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            Console.Clear();

            var mediator = new Mediator();

            // Registrierung
            mediator.RegisterHandler(new GetUserQueryHandler());

            // Senden
            var user = mediator.Send(new GetUserQuery(1));

            Console.WriteLine(user.Name);

            ConsoleMenu.Wait();
        }
    }

    public class UserDto
    {
        public int Id { get; }
        public string Name { get; }

        public UserDto(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    public class GetUserQuery : IRequest<UserDto>
    {
        public int UserId { get; }

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        public UserDto Handle(GetUserQuery request)
        {
            return new UserDto(request.UserId, "Gerhard Ahrens");
        }
    }

}
