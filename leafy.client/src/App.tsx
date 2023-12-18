import './App.css';
import { useState, useEffect } from 'react';

interface Plant {
    id: number,
    name: string,
    latinName: string,
    description: string,
    imageURL: string,
    diseaseId: number,
}

function App() {
    const [plants, setPlants] = useState<Plant[]>([]);

    useEffect(() => {
        const fetchPlants = async () => {
            const response = await fetch(`api/Plants`);
            console.log(response)
            const plants = await response.json();
            setPlants(plants);
        }
        fetchPlants();
    }, []);

    const content = plants === undefined ?
        <p><em>Loading...</em></p> :
        <ul>
            {plants.map(plant =>
                <li key={plant.id}>
                    <h3>{plant.name}</h3>
                    <p>{plant.description}</p>
                    <p>{plant.latinName}</p>
                    <p>{plant.imageURL}</p>
                    <p>{plant.diseaseId}</p>
                </li>
            )}
            </ul>;
    return <div>
        <h1>Plants</h1>
        {content}
    </div>
}

export default App;