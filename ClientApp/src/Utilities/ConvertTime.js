/**/
/*
 * This file contains the utility function to convert C# Style date-time values into easily readable times
 * / 
/**/

const ConvertTime = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toTimeString().substr(0, 8);
}

export default ConvertTime;