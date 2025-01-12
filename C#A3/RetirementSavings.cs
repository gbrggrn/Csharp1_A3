using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace C_A3
{
    /// <summary>
    /// Class responsible for calculating retirement-savings with the user input.
    /// I know, the assignment says no instance variables. However, I believe I have demonstrated that in the WaterIntakeCalculator-class.
    /// I wanted to try working with properties and different levels of accessibility.
    /// </summary>
    internal class RetirementSavings
    {
        public double YearsToRetirement { get; private set; } //public get-property, private set-property. Value can only be manipulated from within this class.
        public double TotalAmount { get; private set; }
        public double TotalInterest {  get; private set; }
        public double TotalInvestment {  get; private set; }
        public double GrowthPercent {  get; private set; }

        public RetirementSavings()
        {
            YearsToRetirement = 0;
            TotalAmount = 0;
            TotalInterest = 0;
            TotalInvestment = 0;
            GrowthPercent = 0;
        }

        /// <summary>
        /// Responsible for running the necessary calculations for the Retirement Savings Calculator.
        /// </summary>
        /// <param name="age">Current age of the future retiree</param>
        /// <param name="retirementAge">The retirement age of the future retiree</param>
        /// <param name="currentSavings">The current retirement savings</param>
        /// <param name="monthlySavings">The monthly savings intended</param>
        /// <param name="interestRate">The monthly interest rate</param>
        public void RunCalculations(double age, double retirementAge, double currentSavings, double monthlySavings, double interestRate)
        {
            int retirementAgeInt = int.Parse(retirementAge.ToString()); //Parse retirement age to an int
            int ageInt = int.Parse(age.ToString()); //Parse current age to an int
            double monthlyInterest = 0; //Declare variable "monthlyInterest" and initialize to "0"

            int months = (retirementAgeInt - ageInt) * 12; //Calculate number of months until retirement

            YearsToRetirement = retirementAge - age; //Calculate number of years until retirement

            TotalAmount = currentSavings; //Set initial total amount to current savings

            for (int i = 0; i < months; i++) //Loop through each month
            {
                TotalInvestment += monthlySavings; //Add monthly savings to TotalInvestment
                monthlyInterest = TotalAmount * interestRate; //Calculate interest for each month
                TotalInterest += monthlyInterest; //Add monthly interest to TotalInterest
                TotalAmount += monthlyInterest + monthlySavings; //Add monthly savings and interest to TotalAmount
            }

            GrowthPercent = TotalInterest / TotalAmount * 100; //Calculate the growth percent
        }
    }
}
