using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebAddessbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;
        private string name;
        private string phones;
        private string emails;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return  Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lastname, Firstname);
        }
        public override string ToString()
        {
            return Lastname + " " + Firstname + " ";
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            int result = Lastname.CompareTo(other.Lastname);
            if (result == 0)
            {
                result = Firstname.CompareTo(other.Firstname);
            }
            return result;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones 
        { 
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                } else
                {
                   return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                return (Name + "\r\n" + Address + "\r\n" + Phones + "\r\n" + "\r\n" + Emails + "\r\n").Trim();
            }
            set
            {
                allInfo = value;
            }
        }

        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                return (CleanUpEmptyName(Firstname) + " " + CleanUpEmptyName(Lastname)).Trim();
            }
            set
            {
                name = value;
            }
        }

        public string Phones
        {
            get
            {
                if (phones != null)
                {
                    return phones;
                }
                return (CleanUpEmpty(HomePhone) + CleanUpEmpty(MobilePhone) + CleanUpEmpty(WorkPhone)).Trim();
            }
            set
            {
                phones = value;
            }
        }

        public string Emails
        {
            get
            {
                if (emails != null)
                {
                    return emails;
                }
                return (CleanUpEmpty(Email) + CleanUpEmpty(Email2) + CleanUpEmpty(Email3)).Trim();
            }
            set
            {
                emails = value;
            }
        }

        private string CleanUpEmpty(string info)
        {
            {
                if (info == null || info == "")
                {
                    return "";
                }
                return info + "\r\n";
            }
        }

        private string CleanUpEmptyName(string nameInfo)
        {
            {
                {
                    if (nameInfo == null || nameInfo == "")
                    {
                        return "";
                    }
                    return nameInfo;
                }
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "")  + "\r\n";
        }

        public string Id { get; set; }

    }
}
