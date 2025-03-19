using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public string HP { 
            get 
            {
                if (HomePhone != "" || HomePhone != null)
                {
                    return CleanUpEmpty("H: ") + CleanUpEmpty(HomePhone);
                }
                else
                {
                    return "";
                }
            }
                set
            {
                HomePhone = value;
            }
                }
        public string MP { 
            get
            {
                if (MobilePhone != "" || MobilePhone != null)
                {
                    return CleanUpEmpty("M: " + MobilePhone);
                }
                else
                {
                    return "";
                }
            }
                set
            {
                MobilePhone = value;
            }
                }
        public string WP {
            get
            {
                if (WorkPhone != "" || WorkPhone  != null)
                {
                    return CleanUpEmpty("W: ") + CleanUpEmpty(WorkPhone);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                WorkPhone = value;
            }
        }
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
                return (Name + "\r\n" + CleanUpEmpty(Address) + "\r\n" + CleanUpEmpty(Phones) + CleanUpEmpty(Emails)).Trim();
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
                return (CleanUpEmpty(HP) + CleanUpEmpty(MP) + CleanUpEmpty(WP));
            }
            set
            {
                phones = value;
            }
        }


        //if (phones != null)
        //{
        //    return phones;
        // } else
        // {
        // return (CleanUpEmpty("H: ") + CleanUpEmpty(HomePhone) + CleanUpEmpty("M: ") + CleanUpEmpty(MobilePhone) + CleanUpEmpty("W: ") + CleanUpEmpty(WorkPhone));
        //}      
        //}
        // set
        //{
        //  phones = value;
        // }
        // }
        //return (CleanUpEmpty(HomePhone) + CleanUpEmpty(MobilePhone) + CleanUpEmpty(WorkPhone));
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
                return info;
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
