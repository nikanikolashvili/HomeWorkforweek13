namespace hm13.Controllers
{
    using hm13.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class AppointmentsController : Controller
    {
        private const string JsonFilePath = "appointments.json";

        [HttpPost]
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid || !IsTimeValid(appointment.Time))
            {
                return BadRequest("At this time the doctor is sleeping, Please select another time.");
            }

            var appointments = await LoadAppointmentsAsync();
            appointments.Add(appointment);
            await SaveAppointmentsAsync(appointments);

            return Ok("Appointment booked successfully.");
        }

        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await LoadAppointmentsAsync();
            return View(appointments); 
        }

        private bool IsTimeValid(string time)
        {
            if (TimeSpan.TryParse(time, out var appointmentTime))
            {
                return appointmentTime.Hours >= 10 && appointmentTime.Hours < 19;
            }
            return false;
        }

        private async Task<List<Appointment>> LoadAppointmentsAsync()
        {
            if (!System.IO.File.Exists(JsonFilePath))
                return new List<Appointment>();

            var json = await System.IO.File.ReadAllTextAsync(JsonFilePath);
            return JsonSerializer.Deserialize<List<Appointment>>(json) ?? new List<Appointment>();
        }

        private async Task SaveAppointmentsAsync(List<Appointment> appointments)
        {
            var json = JsonSerializer.Serialize(appointments);
            await System.IO.File.WriteAllTextAsync(JsonFilePath, json);
        }
    }

}
