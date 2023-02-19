using ConsoleApp116.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp116.HelperModels
{
    class GarageAuto
    {
        private AutoDBContext db;

        public bool CheckCanAddAuto()
        {
            try
            {
                using (AutoDBContext db = new AutoDBContext())
                {
                    return db.Models.Count() > 0 &&
                        db.Colors.Count() > 0 &&
                        db.Types.Count() > 0;
                }               
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Check add auto error: {ex.Message}");
            }
            return false;
        }
        
        public void ShowMainMenu()
        {
            int menu = -1;
            while (true)
            {
                try   
                {
                    Console.Clear();
                    Console.WriteLine("1 - Color");
                    Console.WriteLine("2 - Model");
                    Console.WriteLine("3 - Type");
                    Console.WriteLine("4 - Auto");
                    Console.WriteLine("5 - Exit");
                    menu = Int32.Parse(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            WorkWithColor();
                            break;
                        case 2:
                            WorkWithModels();
                            break;
                        case 3:
                            WorkWithType();
                            break;
                        case 4:
                            WorkWithAuto();
                            break;
                        case 5:
                            Console.WriteLine("I will close");
                            return;
                        default:
                            Console.WriteLine("The selected menu item does not exist");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error main menu method: {ex.Message}");
                }
                Console.ReadKey();
            }
        }

        public void ShowColors()
        {
            try
            {
                using (db = new AutoDBContext())
                {
                    var color_list = db.Colors.ToList();
                    if (color_list.Count == 0)
                    {
                        Console.WriteLine("There is no colors in database");
                        return;
                    }
                    Console.WriteLine("Colors in database (ID - NAME):");
                    foreach (var i in color_list)
                        Console.WriteLine($"{i.ID} - {i.Name}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error show color method: {ex.Message}");
            }
        }
        public void ShowTypes()
        {
            try
            {
                using (db = new AutoDBContext())
                {
                    var type_list = db.Types.ToList();
                    if (type_list.Count == 0)
                    {
                        Console.WriteLine("There is no types in database");
                        return;
                    }
                    Console.WriteLine("Types in database (ID - NAME):");
                    foreach (var i in type_list)
                        Console.WriteLine($"{i.ID} - {i.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error show types method: {ex.Message}");
            }
        }
        public void ShowModels()
        {
            try
            {
                using (db = new AutoDBContext())
                {
                    var model_list = db.Models.ToList();
                    if (model_list.Count == 0)
                    {
                        Console.WriteLine("There is no models in database");
                        return;
                    }
                    Console.WriteLine("Models in database (ID - NAME):");
                    foreach (var i in model_list)
                        Console.WriteLine($"{i.ID} - {i.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error show model method: {ex.Message}");
            }
        }
        public void ShowAutos()
        {
            try
            {
                using (db = new AutoDBContext())
                {
                    var auto_list = db.Autos.Select(a => new
                    {
                        ID = a.ID,
                        Number = a.Number,
                        Color = a.Color.Name,
                        Type = a.Type.Name,
                        Model = a.Model.Name
                    }).ToList();
                    if (auto_list.Count == 0)
                    {
                        Console.WriteLine("There is no autos in database");
                        return;
                    }
                    Console.WriteLine("Autos in database (ID - Number - Color - Type - Model):");
                    foreach (var i in auto_list)
                        Console.WriteLine($"{i.ID} - {i.Number} - {i.Color} - {i.Type} - {i.Model}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error show auto method: {ex.Message}");
            }
        }
        
        public void WorkWithColor()
        {
            int color_menu = -1;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("1 - Show Colors");
                    Console.WriteLine("2 - Add Color");
                    Console.WriteLine("3 - Delete Color");
                    Console.WriteLine("4 - Update Color");
                    Console.WriteLine("5 - Main Menu");
                    color_menu = Int32.Parse(Console.ReadLine());
                    switch (color_menu)
                    {
                        case 1:
                            ShowColors();
                            break;
                        case 2:
                            string name;
                            Console.WriteLine("Enter a label for color");
                            name = Console.ReadLine();
                            using (db = new AutoDBContext())
                            {
                                ColorAuto color_for_add = new ColorAuto(name);

                                var results = new List<ValidationResult>();
                                var context = new ValidationContext(color_for_add);
                                if (!Validator.TryValidateObject(color_for_add, context, results, true))
                                {
                                    foreach (var error in results)
                                    {
                                        Console.WriteLine(error.ErrorMessage);
                                    }
                                }
                                else
                                {
                                    db.Colors.Add(color_for_add);
                                    db.SaveChanges();
                                    Console.WriteLine($"Color {name} was added");
                                }
                            }
                            break;
                        case 3:
                            Console.WriteLine("What color do you want to delete?");
                            ShowColors();
                            int color_id_to_delete = -1;
                            Console.WriteLine("Enter color ID?");
                            color_id_to_delete = Int32.Parse(Console.ReadLine());
                            using (db = new AutoDBContext())
                            {
                                ColorAuto color = db.Colors.Find(color_id_to_delete);
                                if (color != null)
                                {
                                    db.Colors.Remove(color);
                                    db.SaveChanges();
                                    Console.WriteLine($"Color {color} was deleted");
                                }
                                else
                                    Console.WriteLine("Error color ID");
                            }
                            break;
                        case 4:
                            Console.WriteLine("What color do you want to update?");
                            ShowColors();
                            int color_id_to_update = -1;
                            Console.WriteLine("Enter color ID?");
                            color_id_to_update = Int32.Parse(Console.ReadLine());
                            using (db = new AutoDBContext())
                            {
                                ColorAuto color = db.Colors.Find(color_id_to_update);
                                if (color != null)
                                {
                                    Console.WriteLine($"Old name of color - {color}. Enter new name");
                                    string new_color_name = Console.ReadLine();

                                    if (color.Name != new_color_name)
                                    {
                                        color.Name = new_color_name;

                                        var results = new List<ValidationResult>();
                                        var context = new ValidationContext(color);
                                        if (!Validator.TryValidateObject(color, context, results, true))
                                        {
                                            foreach (var error in results)
                                            {
                                                Console.WriteLine(error.ErrorMessage);
                                            }
                                        }
                                        else
                                        {
                                            db.SaveChanges();
                                            Console.WriteLine($"Color was updated. New name is {new_color_name}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("The names are the same");
                                    }
                                }
                                else
                                    Console.WriteLine("Error color ID");
                            }
                            break;
                        case 5:
                            Console.WriteLine($"Let's go to main menu");
                            return;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Color method error: {ex.Message}");
                }
                Console.ReadKey();
            }
        }
        public void WorkWithType()
        {
            int type_menu = -1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Show type");
                Console.WriteLine("2 - Add type");
                Console.WriteLine("3 - Delete type");
                Console.WriteLine("4 - Update type");
                Console.WriteLine("5 - Main Menu");
                type_menu = Int32.Parse(Console.ReadLine());
                switch (type_menu)
                {
                    case 1:
                        ShowTypes();
                        break;
                    case 2:
                        string name;
                        Console.WriteLine("Enter a label for type");
                        name = Console.ReadLine();
                        using (db = new AutoDBContext())
                        {
                            TypeAuto type_for_add = new TypeAuto(name);

                            var results = new List<ValidationResult>();
                            var context = new ValidationContext(type_for_add);
                            if (!Validator.TryValidateObject(type_for_add, context, results, true))
                            {
                                foreach (var error in results)
                                {
                                    Console.WriteLine(error.ErrorMessage);
                                }
                            }
                            else
                            {
                                db.Types.Add(type_for_add);
                                db.SaveChanges();
                                Console.WriteLine($"Type {name} was added");
                            }  
                        }
                        break;
                    case 3:
                        Console.WriteLine("What type do you want to delete?");
                        ShowTypes();
                        int type_id_to_delete = -1;
                        Console.WriteLine("Enter type ID?");
                        type_id_to_delete = Int32.Parse(Console.ReadLine());
                        using (db = new AutoDBContext())
                        {
                            TypeAuto type = db.Types.Find(type_id_to_delete);
                            if (type != null)
                            {
                                db.Types.Remove(type);
                                db.SaveChanges();
                                Console.WriteLine($"Type {type} was deleted");
                            }
                            else
                                Console.WriteLine("Error type ID");
                        }
                        break;
                    case 4:
                        Console.WriteLine("What type do you want to update?");
                        ShowTypes();
                        int type_id_to_update = -1;
                        Console.WriteLine("Enter type ID?");
                        type_id_to_update = Int32.Parse(Console.ReadLine());
                        using (db = new AutoDBContext())
                        {
                            TypeAuto type = db.Types.Find(type_id_to_update);
                            if (type != null)
                            {
                                Console.WriteLine($"Old name of type - {type}. Enter new name");
                                string new_type_name = Console.ReadLine();
                                type.Name = new_type_name;
                                var results = new List<ValidationResult>();
                                var context = new ValidationContext(type);
                                if (!Validator.TryValidateObject(type, context, results, true))
                                {
                                    foreach (var error in results)
                                    {
                                        Console.WriteLine(error.ErrorMessage);
                                    }
                                }
                                db.SaveChanges();
                                Console.WriteLine($"Type was update. New name is {new_type_name}");
                            }
                            else
                                Console.WriteLine("Error type ID");
                        }
                        break;
                    case 5:
                        Console.WriteLine($"Let's go to main menu");
                        return;
                }
                Console.ReadKey();
            }
        }
        public void WorkWithModels()
        {
            int model_menu = -1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Show models");
                Console.WriteLine("2 - Add model");
                Console.WriteLine("3 - Delete model");
                Console.WriteLine("4 - Update model");
                Console.WriteLine("5 - Main Menu");
                model_menu = Int32.Parse(Console.ReadLine());
                switch (model_menu)
                {
                    case 1:
                        ShowModels();
                        break;
                    case 2:
                        string name;
                        Console.WriteLine("Enter a label for model");
                        name = Console.ReadLine();
                        using (db = new AutoDBContext())
                        {
                            ModelAuto model_for_add = new ModelAuto(name);
                            db.Models.Add(model_for_add);
                            db.SaveChanges();
                            Console.WriteLine($"Model {name} was added");
                        }
                        break;
                    case 3:
                        Console.WriteLine("What model do you want to delete?");
                        ShowModels();
                        int model_id_to_delete = -1;
                        Console.WriteLine("Enter model ID?");
                        model_id_to_delete = Int32.Parse(Console.ReadLine());
                        using (db = new AutoDBContext())
                        {
                            ModelAuto model = db.Models.Find(model_id_to_delete);
                            if (model != null)
                            {
                                db.Models.Remove(model);
                                db.SaveChanges();
                                Console.WriteLine($"Model {model} was deleted");
                            }
                            else
                                Console.WriteLine("Error model ID");
                        }
                        break;
                    case 4:
                        Console.WriteLine("What model do you want to update?");
                        ShowModels();
                        int model_id_to_update = -1;
                        Console.WriteLine("Enter model ID?");
                        model_id_to_update = Int32.Parse(Console.ReadLine());
                        using (db = new AutoDBContext())
                        {
                            ModelAuto model = db.Models.Find(model_id_to_update);
                            if (model != null)
                            {
                                Console.WriteLine($"Old name of model - {model}. Enter new name");
                                string new_model_name = Console.ReadLine();
                                model.Name = new_model_name;
                                var results = new List<ValidationResult>();
                                var context = new ValidationContext(model);
                                if (!Validator.TryValidateObject(model, context, results, true))
                                {
                                    foreach (var error in results)
                                    {
                                        Console.WriteLine(error.ErrorMessage);
                                    }
                                }
                                db.SaveChanges();
                                Console.WriteLine($"Type was update. New name is {new_model_name}");
                            }
                            else
                                Console.WriteLine("Error model ID");
                        }
                        break;
                    case 5:
                        Console.WriteLine($"Let's go to main menu");
                        return;
                }
                Console.ReadKey();
            }
        }
        public void WorkWithAuto()
        {
            int auto_menu = -1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Show all autos");
                Console.WriteLine("2 - Add auto");
                Console.WriteLine("3 - Delete auto");
                Console.WriteLine("4 - Update auto");
                Console.WriteLine("5 - Main Menu");
                auto_menu = Int32.Parse(Console.ReadLine());
                switch (auto_menu)
                {
                    case 1:
                        ShowAutos();
                        break;
                    case 2:
                        if (!CheckCanAddAuto())
                        {
                            Console.WriteLine("Not enough data");
                            return;
                        }
                        string auto_number;
                        Console.WriteLine("Enter a number for auto");
                        auto_number = Console.ReadLine();

                        int auto_color;
                        Console.WriteLine("Enter a color for auto (select ID)");
                        ShowColors();
                        auto_color = Int32.Parse(Console.ReadLine());

                        int auto_type;
                        Console.WriteLine("Enter a type for auto (select ID)");
                        ShowTypes();
                        auto_type = Int32.Parse(Console.ReadLine());

                        int auto_model;
                        Console.WriteLine("Enter a model for auto (select ID)");
                        ShowModels();
                        auto_model = Int32.Parse(Console.ReadLine());

                        using (db = new AutoDBContext())
                        {
                            Auto search_in_db = db.Autos.Where(a => a.Number == auto_number &&
                            a.ID_Color == auto_color && a.ID_Model == auto_model && a.ID_Type == auto_type).FirstOrDefault();
                            
                            if (search_in_db != null)
                            {
                                Console.WriteLine("Error. Duplicate data in database.");
                                break;
                            }

                            Auto auto = new Auto(auto_number, auto_color, auto_type, auto_model);
                            
                            db.Autos.Add(auto);
                            db.SaveChanges();
                            Console.WriteLine($"New auto was added");
                        }
                        break;
                    case 3:
                        Console.WriteLine("What auto do you want to delete?");
                        ShowAutos();
                        int auto_id_to_delete = -1;
                        Console.WriteLine("Enter auto ID?");
                        auto_id_to_delete = Int32.Parse(Console.ReadLine());
                        using (db = new AutoDBContext())
                        {
                            Auto auto = db.Autos.Find(auto_id_to_delete);
                            if (auto != null)
                            {
                                db.Autos.Remove(auto);
                                db.SaveChanges();
                                Console.WriteLine($"Auto {auto} was deleted");
                            }
                            else
                                Console.WriteLine("Error auto ID");
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("What auto do you want to update?");
                            int auto_id_to_update = -1;

                            using (AutoDBContext db = new AutoDBContext())
                            {
                                ShowAutos();
                                Console.WriteLine("Enter auto ID to Update");
                                auto_id_to_update = Int32.Parse(Console.ReadLine());
                                Auto auto = db.Autos.Find(auto_id_to_update);
                                if (auto_id_to_update == auto.ID)
                                {
                                    ShowColors();
                                    int color_id_to_update = -1;
                                    Console.WriteLine("Enter color ID to update");
                                    color_id_to_update = Int32.Parse(Console.ReadLine());

                                    ColorAuto color = db.Colors.Find(color_id_to_update);
                                    if (color != null)
                                    {
                                        auto.Color = color;
                                        var results = new List<ValidationResult>();
                                        var context = new ValidationContext(color);
                                        if (!Validator.TryValidateObject(color, context, results, true))
                                        {
                                            foreach (var error in results)
                                            {
                                                Console.WriteLine(error.ErrorMessage);
                                            }
                                        }
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error color ID");
                                    }

                                    ShowTypes();
                                    int type_id_to_update = -1;
                                    Console.WriteLine("Enter type ID to update");
                                    type_id_to_update = Int32.Parse(Console.ReadLine());

                                    TypeAuto type = db.Types.Find(type_id_to_update);
                                    if (type != null)
                                    {
                                        auto.Type = type;
                                        var results = new List<ValidationResult>();
                                        var context = new ValidationContext(type);
                                        if (!Validator.TryValidateObject(type, context, results, true))
                                        {
                                            foreach (var error in results)
                                            {
                                                Console.WriteLine(error.ErrorMessage);
                                            }
                                        }
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error type ID");
                                    }


                                    ShowModels();
                                    int model_id_to_update = -1;
                                    Console.WriteLine("Enter model ID to update");

                                    model_id_to_update = Int32.Parse(Console.ReadLine());

                                    ModelAuto model = db.Models.Find(model_id_to_update);
                                    if (model != null)
                                    {
                                        auto.Model = model;
                                        var results = new List<ValidationResult>();
                                        var context = new ValidationContext(model);
                                        if (!Validator.TryValidateObject(model, context, results, true))
                                        {
                                            foreach (var error in results)
                                            {
                                                Console.WriteLine(error.ErrorMessage);
                                            }
                                        }
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error model ID");
                                    }


                                }
                            }
                        }
                        break;



                    case 5:
                        Console.WriteLine($"Let's go to main menu");
                        return;
                    default:
                        Console.WriteLine("The selected menu item does not exist");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}