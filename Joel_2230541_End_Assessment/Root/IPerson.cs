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
    internal interface IPerson
    {
        //int GetID();
        string GetFirstName();
        string GetSurname();
        string GetPhoneNumber();
        string GetEmailAddress();
        DateTime GetDoB();
        string GetPhysicalAddress();
        void DisplayDetails();
    }
}
