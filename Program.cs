
using HW_005_04_09_2024;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Emit;

class Program
{
    static void Main()
    {
        //Создайте систему для управления «Событиями» и «Гостями». 
        //Создайте таблицы для событий, гостей и связывающую таблицу, которая представляет собой 
        //отношение «многие ко многим» между событиями и гостями. Дополнительно в этой связи создать
        //дополнительную колонку для хранения «Роли гостя» на конкретном событии.
        //Выполните следующие запросы к созданным таблицам(каждый запрос оформите в отдельный метод):

        //1) Добавление гостя на событие.
        //2) Получение списка гостей на конкретном событии.
        //3) Изменение роли гостя на событии.
        //4) Получение всех событий для конкретного гостя.
        //5) Удаление гостя с события.
        //6) Получение всех событий, на которых гость выступал в роли спикера.

        //using (ApplicationContext db = new ApplicationContext())
        //{
        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();
        //}

        //AddGuestToEvent(2, 1, 2);
        //var guestList = GetGuestsByEvent(1);
        //ChangeGuestRole(2, 1, 3);
        //var EventList = GetEventsByGuest(1);
        //RemoveGuestFromEvent(2, 1);
        var EventsGuestInSpeakerRole = GetEventsWhereGuestWasSpeaker(1);
    }
    //1) Добавление гостя на событие.
    public static void AddGuestToEvent(int eventId, int guestId, int roleId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var currentGuest = db.Guests.FirstOrDefault(g => g.Id == guestId);
            if (currentGuest != null)
            {
                var currentEvent = db.Events.FirstOrDefault(e => e.Id == eventId);
                if (currentEvent != null)
                {
                    var currentRole = db.Roles.FirstOrDefault(e => e.Id == roleId);
                    if (currentRole != null)
                    {
                        var eventGuest = new EventGuest
                        {
                            EventId = eventId,
                            GuestId = guestId,
                            GuestRoleId = roleId
                        };
                        db.EventGuests.Add(eventGuest);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
    //2) Получение списка гостей на конкретном событии.
    public static List<Guest> GetGuestsByEvent(int eventId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.EventGuests
                .Where(eg => eg.EventId == eventId)
                .Select(eg => eg.Guest)
                .ToList();
        }
    }
    //3) Изменение роли гостя на событии.
    public static void ChangeGuestRole(int eventId, int guestId, int newRoleId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var eventGuest = db.EventGuests
                .FirstOrDefault(eg => eg.EventId == eventId && eg.GuestId == guestId);
            var role = db.Roles.FirstOrDefault(r=>r.Id ==newRoleId);
            if (eventGuest != null && role !=null)
            {
                eventGuest.GuestRoleId = newRoleId;
                db.SaveChanges();
            }
        }
    }
    //4) Получение всех событий для конкретного гостя.
    public static List<Event> GetEventsByGuest(int guestId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var user = db.Guests.FirstOrDefault(g => g.Id == guestId);
            if (user != null)
                return db.EventGuests
                .Where(eg => eg.GuestId == guestId)
                .Select(eg => eg.Event)
                .ToList();
            return null;
        }
    }
    //5) Удаление гостя с события.
    public static void RemoveGuestFromEvent(int eventId, int guestId)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            var eventGuest = context.EventGuests
                .FirstOrDefault(eg => eg.EventId == eventId && eg.GuestId == guestId);
            if (eventGuest != null)
            {
                context.EventGuests.Remove(eventGuest);
                context.SaveChanges();
            }
        }
    }
    //6) Получение всех событий, на которых гость выступал в роли спикера.
    public static List<Event> GetEventsWhereGuestWasSpeaker(int guestId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var user = db.Guests.FirstOrDefault(g=>g.Id == guestId);
            if (user != null)
                return db.EventGuests
                    .Where(eg => eg.GuestId == guestId && eg.Role.RoleName == "Speaker")
                    .Select(eg => eg.Event)
                    .ToList();
            return null;
        }
    }
}
