﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddessbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; set; }
        
        [Column(Name = "id")]
        public string ContactId { get; set; }

        public static List<GroupContactRelation> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from ctog in db.GCR select ctog).ToList();
            }
        }
    }
}
