using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class ContactViewModel
    {
        //fields - n/a

        //props - create required fields for user to enter information to send us a message

        //create the properties with get & set like we do for methods FIRST, and then add required metadata as annotation ABOVE props

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is Required")] // Adds error message to user to make sure they enter data in this field
        [DataType(DataType.EmailAddress)]//
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [UIHint("MultilineText")] // This makes the Message input a TextArea (bigger than a normal textbox)
        public string Message { get; set; }

        //ctors

        //methods


    }//end class
}//end namespace