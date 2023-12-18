import { useState, useEffect } from 'react';

interface Plant {
    id: number,
    name: string,
    latinName: string,
    description: string,
    imageURL: string,
    diseaseId: number,
}

interface PlantState {
    plants: Plant[] | null,
    loading: boolean,
    error: string | null
}

const usePlants = (): PlantState => {
    const [state, setState] = useState<PlantState>({
        plants: null,
        loading: true,
        error: null
    });

    useEffect(() => {
        const fetchPlants = async () => {
            try {
                const response = await fetch(`api/Plants`);
                const plants = await response.json();
                setState({ plants, loading: false, error: null });
            }
            catch (error) {
                setState({ plants: null, loading: false, error: error.message })
            }
        }
        fetchPlants();
    }, []);

    return state;
};

export default function PlantList() {
    const { plants, loading, error } = usePlants();

    if (loading) {
        return <div>Loading...</div>
    }

    if (error) {
        return <div>{error}</div>
    }

    return (
        <ul>
            <li>
                {plants?.map((plant) => (
                    <div key={plant.id}>
                        <p>{plant.name} </p>
                        <p>{plant.latinName}</p>
                        <p>{plant.description}</p>
                        <p>{plant.diseaseId}</p>
                        <p>{plant.imageURL}</p>
                    </div>
                ))}
            </li>
        </ul>
    )
}