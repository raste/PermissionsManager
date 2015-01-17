// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using DataAccess;

namespace BusinessLayer
{
    public class Tools
    {

        public static void CheckObjectContext(Entities objectContext)
        {
            if (objectContext == null)
            {
                throw new ArgumentNullException("objectContext");
            }
        }

        public static void Save(Entities objectContext)
        {
            CheckObjectContext(objectContext);

            objectContext.SaveChanges();
        }

        public static bool ValidationPassed(ValidationField field, ref string text, long minLength
            , long maxLength, out string error, bool canStartWithSpaces)
        {
            if (minLength > maxLength)
            {
                throw new ArgumentException("minLength > maxLength");
            }

            if (minLength < 0)
            {
                throw new ArgumentException("minLength < 0");
            }

            if (!string.IsNullOrEmpty(text))
            {
                while (text.Length > 0 && text.EndsWith(" ", StringComparison.InvariantCultureIgnoreCase))
                {
                    text = text.Remove(text.Length - 1);
                }
            }

            string small = string.Empty;
            string capital = string.Empty;
            string single = string.Empty;

            switch (field)
            {
                case ValidationField.Name:
                    small = "името";
                    capital = "Името";
                    single = "име";
                    break;
                case ValidationField.Password:
                    small = "паролата";
                    capital = "Паролата";
                    single = "парола";
                    break;
                case ValidationField.Username:
                    small = "потребителското име";
                    capital = "Потребителското име";
                    single = "потребителско име";
                    break;
                case ValidationField.RoleName:
                    small = "името на ролята";
                    capital = "Името на ролята";
                    single = "име на ролята";
                    break;
                case ValidationField.Description:
                    small = "описанието";
                    capital = "Описанието";
                    single = "описание";
                    break;
                default:
                    small = "полето";
                    capital = "Полето";
                    single = "текст в полето";
                    break;
            }

            bool passed = false;
            error = string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                if (minLength < 1)
                {
                    passed = true;
                }
                else
                {
                    error = string.Format("Въведете {0}.\n", single);
                }
            }
            else
            {
                if (canStartWithSpaces == false && text.StartsWith(" ", StringComparison.InvariantCultureIgnoreCase))
                {
                    error = string.Format("{0} не трябва да започва с празно място.\n", capital);
                }
                else if (text.Length < minLength)
                {
                    error = string.Format("Дължината на {0} не трябва да е по-малка от {1}.\n", small, minLength);
                }
                else if (text.Length > maxLength)
                {
                    error = string.Format("Дължината на {0} не трябва да е по-голяма от {1}.\n", small, maxLength);
                }
                else
                {
                    passed = true;
                }
            }

            return passed;
        }

    }
}
