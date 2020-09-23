import React, {useState, useRef, useEffect} from "react";

const NumOfEntries = (props) => {
    let errorMessageRef = useRef("");
    const invalidEntryRef = useRef();
    const inputRef = useRef();
    const [numOfEntries, setNumOfEntries] = useState(10);
    
    const handleChange = (e) => {
        setNumOfEntries(e.target.value);
        let errorMessage = checkEntryValidity(e.target.value);
        
        //check for one space
        if(errorMessage !== "") {
            invalidEntryRef.current.classList.remove("hidden");
            if(!invalidEntryRef.current.classList.contains("visible")) {
                invalidEntryRef.current.classList.add("visible")
            }
            if(!inputRef.current.classList.contains("invalid-entry")) {
                inputRef.current.classList.add("invalid-entry")
            }
            errorMessageRef.current = errorMessage;
        } else {
            if(!invalidEntryRef.current.classList.contains("hidden")) {
                invalidEntryRef.current.classList.add("hidden");
            }
            invalidEntryRef.current.classList.remove("visible");
            inputRef.current.classList.remove("invalid-entry")
            
        }
        
    }
    return (
        <div className="num-of-entries">
            Show 
            <input type="text" ref={inputRef} value={numOfEntries} className="" id="num-of-entries-input" onChange={handleChange} />
            entries
            <div ref={invalidEntryRef} className="text-danger invalid-entry hidden">{errorMessageRef.current}</div>
        </div>
    );
}


const checkEntryValidity = (entry) => {
    let num = parseInt(entry);
    if(isNaN(entry)) {
        return "You must enter a valid number";
    } else if(isNaN(num)) {
        return "You must enter a valid number";
    } else if(num < 0) {
        return "The number of entries to show cannot be less than 0";
    } else if(num > 100) {
        return "Cannot show more than 100 entries at a time.";
    } else {
        return "";
    }

}

export default NumOfEntries;