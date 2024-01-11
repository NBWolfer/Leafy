import React, { useState, useEffect } from "react";
import ImageUpload from "../Components/ImageUpload.tsx";
import axios from "axios";
import "../assets/scanplants.css";

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

    const temp = result.output === undefined ? "Loading..." : result.output;

    return (
        <div className="scanplants">
            <div className="result">
            <h2>Sonuç:{temp}</h2>
            </div>
            <div className="photo">
                 {/* Pass the callback function to ImageUpload */}
                <ImageUpload onBase64DataChange={handleBase64DataChange} />
            </div>
           
        </div>
    );
}

export default Scanplant;
