import "./styles.css";
import { useEffect, useState } from "react";

function ProgressBar({value, color}){
    const [progress, setProgess] = useState(value);

    useEffect(() => {
        if(value >= 0 && value <= 100){
            setProgess(value * 10)
        }else{
            console.error("Invalid value for progress bar")
        }
    }, [value]);

    const progressBarStyle = {
        width: `${progress}%`,
       
        backgroundColor: color || '#007bff', // Default blue color
    
    }

    return(
    
      <div className="progress">
        <div className="progress-bar" style={progressBarStyle} />
    </div>
    );
}

export default ProgressBar;