const ConvertTime = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toTimeString().substr(0, 8);
}

export default ConvertTime;