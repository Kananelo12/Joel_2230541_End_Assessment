/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joel_2230541_End_Assessment.Root
{
    internal class Guardian : Person
    {
        // parameterized constructor
        // initializing the Guardian properties using the superclass object
        public Guardian(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password)
        {
        }
    }
}
