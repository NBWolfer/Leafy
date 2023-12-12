import { useEffect, useState } from 'react';
import './App.css';

interface Plant {
    id: number;
    name: string;
    latinName: string;
    description: string;
    imageUrl: string;
    diseaseId: number;
}

function App() {
    const [plant, setPlant] = useState<Plant[]>();

    useEffect(() => {
        populatePlantData();
    }, []);

    const contents = plant === undefined
        ? <p><em>Loading...</em></p>
        : <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>Name</tr>
                <tr>Latin Name</tr>
                <tr>Description</tr>
                <tr>Image Url</tr>
                <tr>Disease Id</tr>
            </thead>
            <tbody>
                {plant.map(plant =>
                    <tr key={plant.name}>
                        <td>{plant.name}</td>
                        <td>{plant.latinName}</td>
                        <td>{plant.description}</td>
                        <td>{plant.imageUrl}</td>
                        <td>{plant.diseaseId}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Plants</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populatePlantData() {
        const response = await fetch('Plants');
        const data = await response.json();
        setPlant(data);
    }
}

export default App;