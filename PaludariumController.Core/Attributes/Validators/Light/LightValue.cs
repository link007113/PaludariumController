using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PaludariumController.Core.Attributes.Validators.Light
{
    class LightValue : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string testString = value as string;
            bool retVal = false;
            if (testString != null)
            {
                if (testString.Length < 4)
                {

                    int testInt;
                    retVal = int.TryParse(testString, out testInt);
                    if (retVal)
                    {
                        if (testInt <= 0)
                            retVal = false;
                        else if (testInt >= 256)
                            retVal = false;
                    }
                }
            }
            return retVal;
        }
    }
}
