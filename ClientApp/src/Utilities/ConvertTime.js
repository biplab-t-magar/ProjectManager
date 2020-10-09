/**/
/*
 * This file contains the utility function to convert C# Style date-time values into easily readable times
 * / 
/**/


/**/
/*
 * NAME:
 *      ConvertTime() - function to convert C# Style date-time values into easily readable times
 * SYNOPSIS:
 *      ConvertTime(date)   
 *              date --> C# style date
 * DESCRIPTION:
 *      This function converts C# Style date-time values into easily readable times
 * RETURNS
 *      The string containing the easily readable time
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/20/2020 
 * /
 /**/
const ConvertTime = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toTimeString().substr(0, 8);
}

export default ConvertTime;