using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Grundlagen.Models;

namespace Web_Grundlagen.Controllers
{

    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(); //rechtsklick ansichtshinzufügen
        }

        public IActionResult Registrierung()
        {
            return View(new User()
            {
                Birthdate = DateTime.Today,
            });
        }

        // die Formulardaten werden automatisch (Middleware) in die Properties von User kopiert
        [HttpPost]
        public async Task<IActionResult> RegistrierungAsync(User user)
        {
            //Formulardaten überprüfen (validieren)
            if ((user.Name == null) || (user.Name.Trim().Length < 2))
            {
                ModelState.AddModelError("Name", "Name muss mind. 2 Zeichen lang sein!");
            }
            if ((user.Name == null) || (!user.Email.Contains('@')))
            {
                ModelState.AddModelError("Email", "Geben sie bitte eine gültige Mail-Adresse ein");
            }

            // Birthdate ist kein Pfllichtfeld
            //Überprüfung ist notwendig, falls der user ein Geburtsdatum ausgewählt hat ( dann soll es in der Verg. sein)
            if((user.Birthdate != DateTime.Today) &&
                (user.Birthdate >= DateTime.Today))
            {
                ModelState.AddModelError("Birthdate", "Geburtsdatum darf nicht in der Zukunft liegen");
            }
            if (user.Password != user.PasswordRetype)
            {
                ModelState.AddModelError("Password", "Passwörter sind nicht gleich");
            }
            if (ModelState.IsValid)
            {
                //OK --> in DB abspeichern + Meldung ausgeben
                //DB-Teil

                // Hashen des Passworts, vor speichern in DB
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, user.Password);

                using (MyContext context = new MyContext())
                {
                    await context.Users.AddAsync(user);

                    try
                    {
                        int result = await context.SaveChangesAsync();
                        if (result == 1)
                        {
                            return View("Message", new Message()
                            {
                                Title = "Registrierung",
                                MessageText = "Sie wurden erfolgreich Registriert!"
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        // Fehler loggen
                    }
                }
            }
            return View("Message", new Message()
            {
                Title = "Registrierung",
                MessageText = "Fehler bei der Registrierung"
            });
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User loginUser)
                {
                        using (MyContext context = new MyContext())
                        {
                            // Benutzer mit E-Mail-Adresse abrufen
                            var existingUser = await context.Users.FindAsync(loginUser.Email);

                            if (existingUser != null)
                            {
                                // Passwort überprüfen
                                var passwordHasher = new PasswordHasher<User>();
                                var result = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, loginUser.Password);

                                if (result == PasswordVerificationResult.Success)
                                {
                        // Erfolgreich eingeloggt
                        return RedirectToAction("Index", "Home");
                                }
                            }
                    }

            return View("Message", new Message()
            {
                Title = "Login",
                MessageText = "Fehler bei Login"
            });
        }

        public IActionResult showOneUser()
        {
            //die Daten eines Users an die View übergeben

            User u = new User()
            {
                Name = "Testuser",
                Email = "test.test@gamil.com",
                Birthdate = new DateTime(2004, 10, 13)
            };

            User u2 = new User()
            {
                Name = "Testuser",
                Email = "test.test@gamil.com",
                Birthdate = new DateTime(2004, 10, 13)
            };

            // Daten (User   u) an dei View übergeben
            return View(u2);
        }
        public IActionResult showMultipleUsers()
        {
            using (MyContext context = new MyContext())
            {
                var users = context.Users.ToList();
                return View(users);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(String email)
        {
            using (MyContext context = new MyContext())
            {
                var userToDelete = await context.Users.FindAsync(email);
                    context.Users.Remove(userToDelete);
                    await context.SaveChangesAsync();
                    return RedirectToAction("ShowMultipleUsers");
            }
        }


        public async Task<IActionResult> Update(String email)
        {
            using (MyContext context = new MyContext())
            {
                var user = await context.Users.FindAsync(email);
                ViewBag.Mode = "update";
                return View("Registrierung", user);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(User updatedUser)
        {
                using (MyContext context = new MyContext())
                {
                //user mit der EMaliadresse aus DB holen
                //überwsdchreibe diese Daten mit den neune Daten aus updatedUser
                    var user = await context.Users.FindAsync(updatedUser.Email);
                if(user != null)
                {
                    user.Name= updatedUser.Name;
                    user.Birthdate=updatedUser.Birthdate;
                    if (await context.SaveChangesAsync() == 1)
                    {
                        return RedirectToAction("ShowMultipleUsers");
                    }
                }
                return View("Message",new Message() { 
                    Title = "UserUpdate", 
                    MessageText ="User nicht gefunden" 
                });
                
               
                    
            }
         }

    }
}
