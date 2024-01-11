/*import "https://fonts.googleapis.com/css2?family=Materiaal+Symbols+Rounded:opsz,wght,FILL,GRAD@48,400,0,0"*/
import '../assets/plants.css'
//import '../assets/plantss.js'
import { useState, useEffect} from 'react';
import axios from 'axios'
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

interface PlantsProps {
    plantName: string,
    imageUrl: string,
    id: number,
    userName: number,
}

interface BinaryImageProps {
    binaryData: string;
    plantName: string;
}


function NavigateToScan() {window.location.href = "/scanplant";}

function Plants() {
    const [plants, setPlants] = useState<PlantsProps[]>([]);

    useEffect(() => {
        const fetchPlants = async () => {
            await axios.post(`api/UserPlants/UserPlantByUserExpanded`).then(res => {
                console.log(res);
                var result = res.data;
                result.forEach((element: any) => {
                    element.imageUrl = "data:image/j;base64," + element.imageUrl;
                });
                setPlants(res.data);
            }).catch((err) => { console.log(err+" axios i�inden") });
        }
        fetchPlants();
    }, []);

    useEffect(() => {
        const initSlider = async () => {
            console.log("initSlider function called");

            const imageList = await document.querySelector<HTMLElement>(".slider-wrapper .image-list");
            console.log(imageList);
            const slideButtons = await document.querySelectorAll<HTMLElement>(".slider-wrapper .slide-button");
            console.log(slideButtons);
            const sliderScrollbar = await document.querySelector<HTMLElement>(".containerr .slider-scrollbar");
            console.log(sliderScrollbar);
            const scrollbarThumb = await sliderScrollbar.querySelector<HTMLElement>(".scrollbar-thumb");
            console.log(scrollbarThumb);
            const maxScrollLeft = imageList.scrollWidth - imageList.clientWidth;
            console.log(maxScrollLeft);


            //gerekli tüm öğelerin mevcut oldup olmadığının kontrolü eğer bu öğelerden herhangi biri eksikse kodun geri kalanı çalıştırılmaz
            if (imageList && slideButtons.length === 2 && sliderScrollbar && scrollbarThumb) {
                //scrollbar thumb'u sürüklendiğinde görüntü listesinin de hareket etmesi
                scrollbarThumb.addEventListener("mousedown", (e) => {
                    const startX = e.clientX;
                    const thumbPosition = scrollbarThumb.offsetLeft;
                    const maxThumbPosition = sliderScrollbar.getBoundingClientRect().width - scrollbarThumb.offsetWidth;

                    //mouse hareketine göre pozisyon güncelleme
                    const handleMouseMove = (e: MouseEvent) => {
                        const deltaX = e.clientX - startX;
                        const newThumbPosition = thumbPosition + deltaX;
                        //scrollbar thumbın sınırların içinde olması
                        const boundedPosition = Math.max(0, Math.min(maxThumbPosition, newThumbPosition));
                        const scrollPosition = (boundedPosition / maxThumbPosition) * maxScrollLeft;

                        scrollbarThumb.style.left = `${boundedPosition}px`;
                        if (imageList) {
                            imageList.scrollLeft = scrollPosition;
                        }
                    };

                    // Remove event listeners on mouse up
                    const handleMouseUp = () => {
                        document.removeEventListener("mousemove", handleMouseMove);
                        document.removeEventListener("mouseup", handleMouseUp);
                    };

                    // Add event listeners for drag interaction
                    document.addEventListener("mousemove", handleMouseMove);
                    document.addEventListener("mouseup", handleMouseUp);
                });

                //Slidebuttonun ID'sine göre, sağa (-1) veya sola (1) doğru kaydırma işlemi
                slideButtons.forEach((button) => {
                    button.addEventListener("click", () => {
                        const direction = button.id === "prev-slide" ? -1 : 1;
                        const scrollAmount = imageList.clientWidth * direction;
                        if (imageList) {
                            imageList.scrollBy({ left: scrollAmount, behavior: "smooth" });
                        }
                    });
                });

                //listenin başında ise sol button gizleme, listenin sonunda ise sağ button gizleme.
                const handleSlideButtons = () => {
                    if (imageList) {
                        slideButtons[0].style.display = imageList.scrollLeft <= 0 ? "none" : "flex";
                        slideButtons[1].style.display = imageList.scrollLeft >= maxScrollLeft ? "none" : "flex";
                    }
                };

                //scrollbar thumb'u görsel olarak mevcut scroll pozisyonunu yansıtması
                const updateScrollThumbPosition = () => {
                    if (imageList) {
                        const scrollPosition = imageList.scrollLeft;
                        const thumbPosition = (scrollPosition / maxScrollLeft) * (sliderScrollbar.clientWidth - scrollbarThumb.offsetWidth);
                        scrollbarThumb.style.left = `${thumbPosition}px`;
                    }
                };

                //Görüntü listesi kaydırıldığında, scrollbar thumb'un pozisyonu güncellenir ve slide button'larının görünürlüğü kontrol edilir.
                if (imageList) {
                    imageList.addEventListener("scroll", () => {
                        updateScrollThumbPosition();
                        handleSlideButtons();
                    });
                }
            }
        };

        initSlider();

        // Call initSlider on resize and load
        window.addEventListener("resize", initSlider);
        window.addEventListener("load", initSlider);

    }, []);

    const BinaryImage: React.FC<BinaryImageProps> = ({ binaryData, plantName }) => {
        return (
            <div className="image-item-container" key={plantName}>
                <img src={binaryData} alt={plantName} className="image-item" />
                <div className="image-overlay">
                    <p className="cnt">Plant Name: {plantName}</p>
                </div>
            </div>
        );
    };


    return (
        <>
        {plants.length === 0? (<div className='hata'><div>Kullanıcıya ait hiç bir bitki bulunamadı. Bitki eklemek için tarama sayfasına yönlendiriliyorsunuz.</div> <button className='buton_navigate' onClick={NavigateToScan}>click me</button></div>): <div className="containerr">
                <div className="slider-wrapper">
                    <button id="prev-slide" className="slide-button material-symbols-rounded">
                        chevron_left
                    </button>
                    <ul className="image-list">
                        {plants.map(plant =>
                            <div className="image-item-container" id="image-item-container-1" key={plant.id}>
                                <BinaryImage binaryData={plant.imageUrl} plantName={plant.plantName} />;
                                <div className="image-overlay">
                                    <p className="cnt">Plant Name: {plant.plantName}</p>
                                </div>
                            </div>
                        )}
                    </ul>
                    <button id="next-slide" className="slide-button material-symbols-rounded">
                        chevron_right
                    </button>
                </div>
                <div className="slider-scrollbar">
                    <div className="scrollbar-track">
                        <div className="scrollbar-thumb"></div>
                    </div>
                </div>
            </div>}
            


        </>


    )



}

export default Plants;