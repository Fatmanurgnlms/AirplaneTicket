using AirplaneTicketReservationSystem.Model;
using AirplaneTicketReservationSystem;
using ConsoleTableExt;
using AirplaneTicketReservationSystem;
using AirplaneTicketReservationSystem.Model;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hoşgeldiniz...");
        Console.WriteLine("Başlatılıyor...");

        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        basePath = basePath.Replace(@"bin\Debug\net7.0\", @"database\");

        string locationJsonPath = basePath + "location.json";
        string reservationJsonPath = basePath + "reservation.json";
        string aircraftJsonPath = basePath + "aircraft.json";
        string flightJsonPath = basePath + "flight.json";

        CustomJsonService<CustomLocationEntity> locationService = new CustomJsonService<CustomLocationEntity>(locationJsonPath);
        CustomJsonService<CustomReservationEntity> reservationService = new CustomJsonService<CustomReservationEntity>(reservationJsonPath);
        CustomJsonService<CustomAircraftEntity> aircraftService = new CustomJsonService<CustomAircraftEntity>(aircraftJsonPath);
        CustomJsonService<CustomFlightEntity> flightService = new CustomJsonService<CustomFlightEntity>(flightJsonPath);

    MainMenu:
        Console.WriteLine("");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("1 - Location, Reservation, Flight, or Aircraft List");
        Console.WriteLine("2 - Add New Location, Reservation, Flight, or Aircraft Record");
        Console.Write("Select the operation you want to perform: ");
        string result1 = Console.ReadLine();
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        List<CustomLocationEntity>? locationList = locationService.ReadData();
        List<CustomReservationEntity>? reservationList = reservationService.ReadData();
        List<CustomFlightEntity>? flightList = flightService.ReadData();
        List<CustomAircraftEntity>? aircraftList = aircraftService.ReadData();

        if (result1 == "1")
        {
            if (locationList != null)
            {
                Console.WriteLine("LOCATION LIST: ");
                ConsoleTableBuilder.From(locationList).ExportAndWriteLine();
            }

            if (reservationList != null)
            {
                Console.WriteLine("RESERVATION LIST: ");
                ConsoleTableBuilder.From(reservationList).ExportAndWriteLine();
            }

            if (flightList != null)
            {
                Console.WriteLine("FLIGHT LIST: ");
                ConsoleTableBuilder.From(flightList).ExportAndWriteLine();
            }

            if (aircraftList != null)
            {
                Console.WriteLine("AIRCRAFT LIST: ");
                ConsoleTableBuilder.From(aircraftList).ExportAndWriteLine();
            }
        }
        else
        {
            Console.WriteLine("1 - Location");
            Console.WriteLine("2 - Reservation");
            Console.WriteLine("3 - Aircraft");
            Console.WriteLine("4 - Flight");
            Console.Write("Select the area to add new data: ");

            if (int.TryParse(Console.ReadLine(), out int addNew))
            {
                Console.WriteLine("-----------------");

                switch (addNew)
                {
                    case 1:
                        Console.WriteLine("LOCATION ->");
                        CustomLocationEntity location = new CustomLocationEntity();
                        Console.Write("Country: ");
                        location.Country = Console.ReadLine();
                        Console.Write("City: ");
                        location.City = Console.ReadLine();
                        Console.Write("Airport: ");
                        location.Airport = Console.ReadLine();
                        location.IsActive = true;
                        location.Id = locationList == null ? 1 : locationList.Count == 0 ? 1 : locationList.Max(p => p.Id) + 1;

                        locationService.WriteData(location);
                        break;

                    case 2:
                        Console.WriteLine("RESERVATION ->");
                        CustomReservationEntity reservation = new CustomReservationEntity();
                        Console.Write("First Name: ");
                        reservation.FirstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        reservation.LastName = Console.ReadLine();
                        Console.Write("Age: ");
                        if (int.TryParse(Console.ReadLine(), out int age))
                        {
                            reservation.Age = age;
                        }
                        else
                        {
                            Console.WriteLine("Invalid age. Record not added.");
                            break;
                        }
                        Console.Write("Flight (Id): ");
                        if (int.TryParse(Console.ReadLine(), out int flightId))
                        {
                            reservation.FlightId = flightId;
                        }
                        else
                        {
                            Console.WriteLine("Invalid flight ID. Record not added.");
                            break;
                        }
                        reservation.Id = reservationList == null ? 1 : reservationList.Count == 0 ? 1 : reservationList.Max(p => p.Id) + 1;

                        // Flight check
                        CustomFlightEntity? flightInfo = flightList?.FirstOrDefault(c => c.Id == reservation.FlightId);

                        if (flightInfo == null)
                        {
                            Console.WriteLine("Flight information not found!");
                            break;
                        }

                        // Aircraft check
                        CustomAircraftEntity aircraftInfo = aircraftList?.FirstOrDefault(c => c.Id == flightInfo.AircraftId);

                        if (aircraftInfo == null)
                        {
                            Console.WriteLine("Aircraft information not found!");
                            break;
                        }

                        reservationService.WriteData(reservation);
                        break;

                    case 3:
                        Console.WriteLine("AIRCRAFT ->");
                        CustomAircraftEntity aircraft = new CustomAircraftEntity();
                        Console.Write("Brand: ");
                        aircraft.Brand = Console.ReadLine();
                        Console.Write("Model: ");
                        aircraft.Model = Console.ReadLine();
                        Console.Write("Serial Number: ");
                        aircraft.SerialNumber = Console.ReadLine();
                        Console.Write("Seat Count: ");
                        if (int.TryParse(Console.ReadLine(), out int seatCount))
                        {
                            aircraft.SeatCount = seatCount;
                        }
                        else
                        {
                            Console.WriteLine("Invalid seat count. Record not added.");
                            break;
                        }
                        aircraft.Id = aircraftList == null ? 1 : aircraftList.Count == 0 ? 1 : aircraftList.Max(p => p.Id) + 1;

                        aircraftService.WriteData(aircraft);
                        break;

                    case 4:
                        Console.WriteLine("FLIGHT ->");
                        CustomFlightEntity flight = new CustomFlightEntity();
                        Console.Write("Time: ");
                        flight.Time = Console.ReadLine();
                        Console.Write("Location (Id): ");
                        if (int.TryParse(Console.ReadLine(), out int locationId))
                        {
                            flight.LocationId = locationId;
                        }
                        else
                        {
                            Console.WriteLine("Invalid location ID. Record not added.");
                            break;
                        }
                        Console.Write("Aircraft (Id): ");
                        if (int.TryParse(Console.ReadLine(), out int aircraftId))
                        {
                            flight.AircraftId = aircraftId;
                        }
                        else
                        {
                            Console.WriteLine("Invalid aircraft ID. Record not added.");
                            break;
                        }
                        flight.Id = flightList == null ? 1 : flightList.Count == 0 ? 1 : flightList.Max(p => p.Id) + 1;

                        // Location check
                        CustomLocationEntity locationCheck = locationList?.FirstOrDefault(c => c.Id == flight.LocationId);

                        if (locationCheck == null)
                        {
                            Console.WriteLine("Location information not found!");
                            break;
                        }

                        // Aircraft check
                        CustomAircraftEntity aircraftCheck = aircraftList?.FirstOrDefault(c => c.Id == flight.AircraftId);

                        if (aircraftCheck == null)
                        {
                            Console.WriteLine("Aircraft information not found!");
                            break;
                        }

                        flightService.WriteData(flight);
                        break;

                    default:
                        Console.WriteLine("Invalid number selection!!!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        goto MainMenu;
    }
}
