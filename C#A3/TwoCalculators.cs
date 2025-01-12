using System.Reflection.Metadata.Ecma335;

namespace C_A3
{
    /// <summary>
    /// This class is responsible for loading specific UI-elements, handling user input and displaying output.
    /// I added a birth year combo box to the retirement-savings part because using the birth year from the WaterIntakeCalculator did not feel very intuitive.
    /// I wanted to try using a trackbar, that is why it is there.
    /// </summary>
    internal partial class TwoCalculators : Form
    {
        private readonly InputValidation validation;
        private readonly string NotValidInputString = "Not valid input";

        /// <summary>
        /// Initializes a new instance of the TwoCalculators class and assigns an InputValidation instance for validating user input.
        /// </summary>
        /// <param name="validation">A reference to an instance of the InputValidation-class</param>
        public TwoCalculators(InputValidation validation)
        {
            this.validation = validation;
            InitializeComponent();
        }

        /// <summary>
        /// Loads birth years and retirement age into their respective combo-boxes when the form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGUI(object sender, EventArgs e)
        {
            int loadCurrentYear = int.Parse(DateTime.Now.Year.ToString()); //Gets the current year, starting point for the coming loop.

            for (int loadYear = loadCurrentYear; loadYear >= 1900; loadYear--) //Loads the years "backwards" from current year to 1900.
            {
                birthYearBox.Items.Add(loadYear.ToString()); //Updates the boxes for every iteration
                birthYearRetirementBox.Items.Add(loadYear.ToString());
            }

            weightBox.PlaceholderText = "kg"; //Sets placeholder texts for weight/height-boxes
            heightBox.PlaceholderText = "cm";

            int loadRetirementAge = 62;

            for (int loadAge = loadRetirementAge; loadAge <= 70; loadAge++) //Loads retirementages 62-70
            {
                retirementAgeBox.Items.Add(loadAge.ToString());
            }
            
        }

        /// <summary>
        /// Executes when the Water Calculator "Calculate"-button is clicked.
        /// Validates input, calculates the recommended water intake and displays the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateWaterButton_Click(object sender, EventArgs e)
        {
            double heightIn = 0.0; //Declare variable of type double to store eventual "inches"

            bool imperialIfTrue = MetricOrImperial_Check(); //Checks if "metric" or "imperial" is chosen as unit

            validation.ValidateGender(genderFemaleRadioButton, genderOtherRadioButton, genderMaleRadioButton, out Enums.Gender gender); //Checks radio-buttons to assign "gender"

            validation.ValidateActivityLevel(activityBar, out Enums.ActivityLevel activityLevel); //Check radio-buttons to assign "activity level"

            if (ValidateWaterCalculatorInput(out string name, out double height, out double weight, out double age, ref heightIn)) //If all inputs are valid: execute block
            {
                Person person = new(name, age, height, weight, gender, activityLevel); //Declare and initialize new object "person"
                WaterIntakeCalculator waterIntakeCalculator = new(person); //Declare and initialize new object "water calculator"

                double calculatedIntake = waterIntakeCalculator.CalculateWaterIntake(imperialIfTrue, heightIn); //Declare new double to store result of water-intake-calculation

                double calculatedGlasses = waterIntakeCalculator.CalculateNumberOfGlasses(imperialIfTrue, calculatedIntake); //Declare new double to store result of number-of-glasses-calculation

                DisplayWaterCalculatorResult(imperialIfTrue, name, calculatedIntake, calculatedGlasses); //Call print-method to display results
            }
        }

        /// <summary>
        /// Executes when the Retirement Savings "Calculate"-button is used.
        /// Calls validation methods for all inputs.
        /// If all inputs are correct, creates a new RetirementSavings-object and calls its' RunCalculations-method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateSavingsButton_Click(object sender, EventArgs e)
        {
            if (ValidateRetirementSavingsInput(out double age, out double retirementAge, out double currentSavings, out double monthlySavings, out double interestRate)) //If all inputs are valid: execute block
            {
                RetirementSavings retirementSavings = new(); //Declare and initialize new object "retirementSavings"

                retirementSavings.RunCalculations(age, retirementAge, currentSavings, monthlySavings, interestRate); //Call method "RunCalculations" with the validated input as arguments.

                DisplayRetirementSavingsResult(retirementSavings.YearsToRetirement, retirementSavings.TotalAmount, retirementSavings.TotalInterest, retirementSavings.TotalInvestment, retirementSavings.GrowthPercent); //Call print-method to display results using get-properties of retirementSavings-variables.
            }
        }

//Helper methods

        /// <summary>
        /// Checks if the "imperial" radioButton is checked, since "metric" is the default.
        /// </summary>
        /// <returns>true if imperialButton isChecked : false if imperialButton isNotChecked</returns>
        private bool MetricOrImperial_Check()
        {
            if (imperialButton.Checked)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calls validation-methods on all inputs from the Water Intake Calculator.
        /// If validation-methods return false, it displays a faulty-input message in the corresponding box.
        /// If all validations return true, the method returns boolean "waterCalculatorInputIsValid" with the value true.
        /// </summary>
        /// <param name="name">A string to hold the name of a person</param>
        /// <param name="height">A double to hold the height of a person</param>
        /// <param name="weight">A double to hold the weight of a person</param>
        /// <param name="age">A double to hold the age of a person</param>
        /// <param name="heightIn">A double to hold the inches of a persons' height</param>
        /// <returns>true if all inputs are valid : false if one or more input is faulty</returns>
        private bool ValidateWaterCalculatorInput(out string name, out double height, out double weight, out double age, ref double heightIn)
        {
            bool waterCalculatorInputIsValid = true;

            if (!validation.ValidateName(nameBox, out name))
            {
                NotValidInputMessage(nameBox, NotValidInputString);
                waterCalculatorInputIsValid = false;
            }

            if (!validation.ValidateDoubleFromTextBox(heightBox, out height))
            {
                NotValidInputMessage(heightBox, NotValidInputString);
                waterCalculatorInputIsValid = false;
            }

            if (heightBoxIn.Visible == true)
            {
                if (!validation.ValidateDoubleFromTextBox(heightBoxIn, out heightIn))
                {
                    NotValidInputMessage(heightBoxIn, NotValidInputString);
                    waterCalculatorInputIsValid = false;
                }
            }

            if (!validation.ValidateDoubleFromTextBox(weightBox, out weight))
            {
                NotValidInputMessage(weightBox, NotValidInputString);
                waterCalculatorInputIsValid = false;
            }

            if (!validation.ValidateAge(birthYearBox, out age))
            {
                NotValidInputMessageComboBox(birthYearBox, NotValidInputString);
                waterCalculatorInputIsValid = false;
            }

            return waterCalculatorInputIsValid;
        }

        /// <summary>
        /// Calls validation-methods on all inputs from the Retirement Savings Calculator.
        /// If validation-methods return false, it displays a faulty-input message in the corresponding box.
        /// If all validations return true, the method returns a boolean "retirementSavingsInputIsValid" with the value true.
        /// </summary>
        /// <param name="age">A double to hold a persons age</param>
        /// <param name="retirementAge">A double to hold the selected retirement age</param>
        /// <param name="currentSavings">A double to hold the input current savings</param>
        /// <param name="monthlySavings">A double to hold the input monthly savings</param>
        /// <param name="interestRate">A double to hold the input interest rate</param>
        /// <returns>true if all inputs are valid : false if one or more input is faulty</returns>
        private bool ValidateRetirementSavingsInput(out double age, out double retirementAge, out double currentSavings, out double monthlySavings, out double interestRate)
        {
            bool retirementSavingsInputIsValid = true;
            
            if (!validation.ValidateAge(birthYearRetirementBox, out age))
            {
                NotValidInputMessageComboBox(birthYearRetirementBox, NotValidInputString);
                retirementSavingsInputIsValid = false;
            }

            if (!validation.ValidateRetirementAge(retirementAgeBox, out retirementAge))
            {
                NotValidInputMessageComboBox(retirementAgeBox, NotValidInputString);
                retirementSavingsInputIsValid = false;
            }

            if (!validation.ValidateDoubleFromTextBox(currentSavingsBox, out currentSavings))
            {
                NotValidInputMessage(currentSavingsBox, NotValidInputString);
                retirementSavingsInputIsValid = false;
            }

            if (!validation.ValidateDoubleFromTextBox(monthlySavingsBox, out monthlySavings))
            {
                NotValidInputMessage(monthlySavingsBox, NotValidInputString);
                retirementSavingsInputIsValid = false;
            }

            if (!validation.ValidateInterestRate(interestRateBox, out interestRate))
            {
                NotValidInputMessage(interestRateBox, NotValidInputString);
                retirementSavingsInputIsValid = false;
            }

            return retirementSavingsInputIsValid;
        }

        /// <summary>
        /// Print-method to display faulty-input message in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox in which to display the message</param>
        /// <param name="message">The message to be displayed</param>
        private static void NotValidInputMessage(TextBox textBox, string message)
        {
            textBox.PlaceholderText = message;
        }

        /// <summary>
        /// Print-method to display faulty-input message in a ComboBox.
        /// </summary>
        /// <param name="comboBox">The ComboBox in which to display the message</param>
        /// <param name="message">The message to be displayed</param>
        private static void NotValidInputMessageComboBox(ComboBox comboBox, string message)
        {
            comboBox.Text = message;
        }

        /// <summary>
        /// Executes if RadioButton metricButton is actively checked.
        /// Changes the placeholder-texts of heightBox and weightBox.
        /// Sets heightBoxIn (inches, height) visibility to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metricButton_CheckedChanged(object sender, EventArgs e)
        {
            heightBox.PlaceholderText = "cm";
            heightBoxIn.Visible = false;
            weightBox.PlaceholderText = "kg";
        }

        /// <summary>
        /// Executes if RadioButton imperialButton is actively checked.
        /// Changes the placeholder-texts of heightBox and weightBox.
        /// Sets heightBoxIn (inches, height) visibility to true and adds a placeholder-text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imperialButton_CheckedChanged(object sender, EventArgs e)
        {
            heightBox.PlaceholderText = "ft";
            heightBoxIn.PlaceholderText = "in";
            heightBoxIn.Visible = true;
            weightBox.PlaceholderText = "lbs";
        }

        /// <summary>
        /// Responsible for formatting and displaying the output of the Water Calculator.
        /// </summary>
        /// <param name="imperialIfTrue">Boolean to signal if imperial or metric units is chosen</param>
        /// <param name="name">The name of the person</param>
        /// <param name="calculatedIntake">Result of the total calculated water intake</param>
        /// <param name="calculatedGlasses">Result of the calculated number of glasses</param>
        private void DisplayWaterCalculatorResult(bool imperialIfTrue, String name, double calculatedIntake, double calculatedGlasses)
        {
            waterIntakeResultBox.Text = "Recommended daily water consumption for " + name; //Display name as title of "waterIntakeResultBox"

            //waterIntakeDisplayLabel.Visible = true;
            //glassesDisplayLabel.Visible = true;

            if (imperialIfTrue) //if true: display with unit annotation "imperial"
            {
                waterIntakeDisplayLabel.Text = $"{calculatedIntake:F2} ounces!";
                glassesDisplayLabel.Text = $"{calculatedGlasses:F2} glasses!";
            }
            else //else: display with unit annotation "metric"
            {
                waterIntakeDisplayLabel.Text = $"{calculatedIntake:F2} milliliters!";
                glassesDisplayLabel.Text = $"{calculatedGlasses:F2} glasses!";
            }

        }

        /// <summary>
        /// Responsible for formatting and displaying the output of the Retirement Savings Calculator.
        /// </summary>
        /// <param name="yearsToRetirement">Years left to retirement</param>
        /// <param name="totalAmount">Total amount accumulated</param>
        /// <param name="totalInterest">Total interest accumulated</param>
        /// <param name="totalInvestment">Total investement made</param>
        /// <param name="growthPercent">The growth, in percent</param>
        private void DisplayRetirementSavingsResult(double yearsToRetirement, double totalAmount, double totalInterest, double totalInvestment, double growthPercent)
        {
            yearsToRetirementLabel.Text = yearsToRetirement.ToString("F0"); //Print output with 0 decimals

            totalAmountLabel.Text = totalAmount.ToString("F2"); //Print output with 2 decimals

            totalInterestLabel.Text = totalInterest.ToString("F2");

            totalInvestmentLabel.Text = totalInvestment.ToString("F2");

            growthPercentLabel.Text = growthPercent.ToString("F2");
        }
    }
}
