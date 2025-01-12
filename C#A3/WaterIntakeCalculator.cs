using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_A3
{
    /// <summary>
    /// Responsible for calculating the water intake for a person based on a collection of parameters.
    /// </summary>
    /// <param name="person">The instance of person which holds the input values to be used in calculations</param>
    internal class WaterIntakeCalculator (Person person)
    {
        private readonly Person person = person;

        //Constants for readability below
        private const double glassSizeMl = 240.0;
        private const double glassSizeOz = 240.0 * 0.033814;
        private const double mlPerKg = 33.0;
        private const double lbsToKgConversionRate = 0.453592;
        private const double mlToOzConversionRate = 0.033814;

        /// <summary>
        /// Responsible for calculating the water intake for a person based on user input and defined constants.
        /// </summary>
        /// <param name="imperialIfTrue">Imperial units if value true</param>
        /// <param name="heightIn">If imperial units is chosen, this variable contains the height, inches</param>
        /// <returns>The recommended intake in milliliters</returns>
        public double CalculateWaterIntake(bool imperialIfTrue, double heightIn)
        {
            //Declare variables to hold multipliers and base intake in ml
            double heightMultiplier;
            double genderMultiplier;
            double activityMultiplier;
            double ageMultiplier;
            double baseIntakeMl;

            if (imperialIfTrue) //If true: convert imperial units to metric, get height multiplier and calculate base intake
            {
                double weightMetric = person.Weight * lbsToKgConversionRate; //lbs to kg
                double heightMetric = (person.Height * 12.0 + heightIn) * 2.54; //ft&in to in to cm
                heightMultiplier = GetHeightMultiplier(heightMetric);
                baseIntakeMl = weightMetric * mlPerKg;
            }
            else //else: continue with metric units, get height multiplier and calculate base intake
            {
                heightMultiplier = GetHeightMultiplier(person.Height);
                baseIntakeMl = person.Weight * mlPerKg;
            }
            
            //Get the rest of the multipliers
            genderMultiplier = GetGenderMultiplier(person.PersonGender);
            activityMultiplier = GetActivityMultiplier(person.PersonActivityLevel);
            ageMultiplier = GetAgeMultiplier(person.Age);

            //Calculate the water intake using base intake and the multipliers
            double personWaterIntake = baseIntakeMl * genderMultiplier * heightMultiplier * activityMultiplier * ageMultiplier;
            
            if (imperialIfTrue) //If true: convert metric to imperial
            {
                double personWaterIntakeOz = personWaterIntake * mlToOzConversionRate;
                return personWaterIntakeOz;
            }
            else //else: return metric
            {
                return personWaterIntake;
            }
        }

        /// <summary>
        /// Calculates the number of glasses.
        /// </summary>
        /// <param name="imperialIfTrue">Imperial units chosen if value is true</param>
        /// <param name="personWaterIntake">The recommended intake in milliliters</param>
        /// <returns>The number of glasses</returns>
        public double CalculateNumberOfGlasses(bool imperialIfTrue, double personWaterIntake)
        {
            double numberOfGlasses;

            if (imperialIfTrue) //If true: imperial units
            {
                numberOfGlasses = personWaterIntake / glassSizeOz;
                return numberOfGlasses;
            }
            else //else: metric
            {
                numberOfGlasses = personWaterIntake / glassSizeMl;
                return numberOfGlasses;
            }
        }

        /// <summary>
        /// Returns the gender multiplier based on the value of "personGender".
        /// </summary>
        /// <param name="personGender">The persons gender</param>
        /// <returns>The multiplier associated with the gender value</returns>
        public double GetGenderMultiplier(Enums.Gender personGender)
        {
            if (personGender == Enums.Gender.Male)
            {
                return 1.1;
            }

            if (personGender == Enums.Gender.Female)
            {
                return 0.9;
            }

            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Returns the height multiplier based on the value of "height".
        /// </summary>
        /// <param name="height">The persons height</param>
        /// <returns>The height multiplier</returns>
        public double GetHeightMultiplier(double height)
        {
            if (height > 175)
            {
                return 1.05;
            }

            if (height < 160)
            {
                return 0.95;
            }

            if (height <= 160 && height >= 175)
            {
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// Returns the activity level multiplier based on the value of "personActivityLevel".
        /// </summary>
        /// <param name="personActivityLevel">The persons activity level</param>
        /// <returns>The activity level multiplier</returns>
        public double GetActivityMultiplier(Enums.ActivityLevel personActivityLevel)
        {
            if (personActivityLevel == Enums.ActivityLevel.High)
            {
                return 1.5;
            }

            if (personActivityLevel == Enums.ActivityLevel.Mid)
            {
                return 1.2;
            }

            return 1;
        }

        /// <summary>
        /// Returns the age multiplier based on the value of "age".
        /// </summary>
        /// <param name="age">The persons age</param>
        /// <returns>The age multiplier</returns>
        public double GetAgeMultiplier(double age)
        {
            if (age < 30)
            {
                return 1.1;
            }

            if (age > 55)
            {
                return 0.9;
            }

            if (age <=30 && age >= 55)
            {
                return 1;
            }

            return 1;
        }

    }
}
