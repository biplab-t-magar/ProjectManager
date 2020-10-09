/**/
/*
 * This file contains the utility function to convert C# Style date-time values into easily readable dates
 * / 
/**/

/**/
/*
 * NAME:
 *      ConvertDate() - function to convert C# Style date-time values into easily readable dates
 * SYNOPSIS:
 *      ConvertDate(date)   
 *              date --> C# style date
 * DESCRIPTION:
 *      This function converts C# Style date-time values into easily readable dates
 * RETURNS
 *      The string containing the easily readable date
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/20/2020 
 * /
 /**/
const ConvertDate = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toDateString();
}

export default ConvertDate;