import React, { useState, useEffect } from "react";
import ImageUpload from "../Components/ImageUpload.tsx";
import axios from "axios";

interface Plant {
    image: string;
}

interface Result {
    output: string;
}

function Scanplant() {
    const [plant, setPlant] = useState<Plant>({ image: "" });
    const [result, setResult] = useState<Result>({ output: "" });

    const handleBase64DataChange = (base64Data: string | null) => {
        setPlant({ image: base64Data || "" });
    };

    const postPlant = async () => {
        try {
            const imageString = plant.image;
            await axios.post("/api/Image", { image: imageString }).then((res) => {
                if (res.data.message) {
                    console.log(res.data);
                    return
                }
                var result = res.data;
                setResult({ output: result });

                console.log(result);
            });
        } catch (error) {
            console.error("Error posting plant:", error);
        }
    };

    useEffect(() => {
        postPlant();
    }, [plant]);

    return (
        <div className="ScanPlants">
            {/* Pass the callback function to ImageUpload */}
            <h2>Result:{result.output}</h2>
            
            <ImageUpload onBase64DataChange={handleBase64DataChange} />
        </div>
    );
}

export default Scanplant;
