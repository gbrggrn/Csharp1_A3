using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C_A3
{
    /// <summary>
    /// Contains methods to validate user input.
    /// </summary>
    public class InputValidation ()
    {
        /// <summary>
        /// Validates a double entered into a text box.
        /// </summary>
        /// <param name="textBox">The text box containing input</param>
        /// <param name="validatedDouble">The validated double</param>
        /// <returns>true if text box is not empty and input can be parsed as a double : else false</returns>
        public bool ValidateDoubleFromTextBox(TextBox textBox, out double validatedDouble)
        {
            if (!String.IsNullOrEmpty(textBox.Text) && double.TryParse(textBox.Text, out validatedDouble))
            {
                return true;
            }
            else
            {
                validatedDouble = 0.0; //Variable "validatedDouble" needs to be initialized before exiting method
                return false;
            }
        }

        /// <summary>
        /// Validates a name from a text box.
        /// </summary>
        /// <param name="nameBox">The text box containing input</param>
        /// <param name="name">The validated name</param>
        /// <returns>true if text box is not empty and name is not longer than 40 char : else false</returns>
        public bool ValidateName(TextBox nameBox, out string name)
        {
            if (!String.IsNullOrWhiteSpace(nameBox.Text) && nameBox.Text.Length < 40)
            {
                name = nameBox.Text;
                return true;
            }
            else
            {
                name = ""; //Variable "name" needs to be assigned a value before exiting method
                return false;
            }
        }

        /// <summary>
        /// Validates the choice of birth year from a combo box and calculates the persons age.
        /// Assigns value to variable "age" through the out-keyword.
        /// </summary>
        /// <param name="birthYearBox">The combo box containing input</param>
        /// <param name="age">The age of the person</param>
        /// <returns>true if selection is not null and birth year can be parsed as a double : else false</returns>
        public bool ValidateAge(ComboBox birthYearBox, out double age)
        {
            if (birthYearBox.SelectedItem != null && double.TryParse(birthYearBox.GetItemText(birthYearBox.SelectedItem), out double birthyear))
            {
                double currentYear = double.Parse(DateTime.Now.Year.ToString()); //Parse the current year to a double
                age = currentYear - birthyear; //Calculate persons age
                return true; //Return true
            }
            else
            {
                age = 0.0; //Variable "age" needs to be assigned a value before exiting method
                return false;
            }
        }

        /// <summary>
        /// Validates the choice of retirement age from a combo box.
        /// Assigns value to variable "retirementAge" through the out-keyword.
        /// </summary>
        /// <param name="retirementAgeBox">The combo box containing input</param>
        /// <param name="retirementAge">The chosen retirement age</param>
        /// <returns>true if selection is not null and retirement age can be parsed as a double : else false</returns>
        public bool ValidateRetirementAge(ComboBox retirementAgeBox, out double retirementAge)
        {
            if (retirementAgeBox.SelectedItem != null && double.TryParse(retirementAgeBox.GetItemText(retirementAgeBox.SelectedItem), out retirementAge))
            {
                return true;
            }
            else
            {
                retirementAge = 0.0; //Variable "retirementAge" needs to be assigned a value before exiting method
                return false;
            }
        }

        /// <summary>
        /// Validates the interest rate input and calculates the correct format from yearly to monthly interest.
        /// Assigns the formatted value to the "interestRate" variable through the out-keyword.
        /// </summary>
        /// <param name="interestRateBox">The text box containing input</param>
        /// <param name="interestRate">The monthly interest rate</param>
        /// <returns>true if text box is not empty, input can be parsed as double and input is between 0.0 and 100.0 : else false</returns>
        public bool ValidateInterestRate(TextBox interestRateBox, out double interestRate)
        {
            if (!String.IsNullOrWhiteSpace(interestRateBox.Text) && double.TryParse(interestRateBox.Text, out interestRate))
            {
                if (interestRate > 0.0 &&  interestRate < 100.0) //Check if interestRate is between 0.0 and 100.0
                {
                    interestRate = (interestRate / 100) / 12; //Calculate correct monthly format ex: 5% = (5/100)/12
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                interestRate = 0.0; //Variable "interestRate" needs to be assigned a value before exiting method
                return false;
            }
        }

        /// <summary>
        /// Assigns the correct enum value to variable "gender" depending on which radioButton isChecked.
        /// </summary>
        /// <param name="genderFemaleRadioButton">The radio button corresponding to choice of "female"</param>
        /// <param name="genderOtherRadioButton">The radio button corresponding to choice of "other"</param>
        /// <param name="genderMaleRadioButton">The radio button corresponding to choice of "male"</param>
        /// <param name="gender">The selected gender</param>
        public void ValidateGender(RadioButton genderFemaleRadioButton, RadioButton genderOtherRadioButton, RadioButton genderMaleRadioButton, out Enums.Gender gender)
        {
            gender = Enums.Gender.Unknown; //Assign default value

            if (genderFemaleRadioButton.Checked)
            {
                gender = Enums.Gender.Female;
            }

            else if (genderOtherRadioButton.Checked)
            {
                gender = Enums.Gender.Other;
            }

            else if (genderMaleRadioButton.Checked)
            {
                gender = Enums.Gender.Male;
            }
        }

        /// <summary>
        /// Assigns the correct enum value to variable "activityLevel" depending on what choice is made on the track bar.
        /// </summary>
        /// <param name="activityLevelTrackBar">The track bar where the choice is made</param>
        /// <param name="activityLevel">The chosen activity level</param>
        public void ValidateActivityLevel(TrackBar activityLevelTrackBar, out Enums.ActivityLevel activityLevel)
        {
            activityLevel = Enums.ActivityLevel.Unknown; //Assign default value

            switch (activityLevelTrackBar.Value) //Switch statement for clarity
            {
                case 0:
                    activityLevel = Enums.ActivityLevel.Low; //If track bar position "0", assign activity level "low"
                    break;
                case 1:
                    activityLevel = Enums.ActivityLevel.Mid;
                    break;
                case 2:
                    activityLevel = Enums.ActivityLevel.High;
                    break;
            }
        }
    }
}
