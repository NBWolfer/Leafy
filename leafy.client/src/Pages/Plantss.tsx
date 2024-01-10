import rs1 from '../Images/2119927025-0.png'
import rs2 from '../Images/calendula.png'
import rs3 from '../Images/4025565.jpg'
import rs4 from '../Images/623d22d72290ed75f7c45c8ca902f7ba.jpg'
import rs5 from '../Images/icons8-close-100.png'
import rs6 from '../Images/personal-growth.png'
/*import "https://fonts.googleapis.com/css2?family=Materiaal+Symbols+Rounded:opsz,wght,FILL,GRAD@48,400,0,0"*/
import '../assets/plants.css'
//import '../assets/plantss.js'
import { useEffect, useState } from 'react';

//interface PlantsProps {
//    name: string,
//    img: string,
//}

function Plants() {

    // const [plants, setPlants] = useState<PlantsProps[]>([]);

    // useEffect(() => {
        
    // });

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

            if (imageList && slideButtons.length === 2 && sliderScrollbar && scrollbarThumb) {
                // Handle scrollbar thumb drag
                scrollbarThumb.addEventListener("mousedown", (e) => {
                    const startX = e.clientX;
                    const thumbPosition = scrollbarThumb.offsetLeft;
                    const maxThumbPosition = sliderScrollbar.getBoundingClientRect().width - scrollbarThumb.offsetWidth;

                    // Update thumb position on mouse move
                    const handleMouseMove = (e: MouseEvent) => {
                        const deltaX = e.clientX - startX;
                        const newThumbPosition = thumbPosition + deltaX;
                        // Ensure the scrollbar thumb stays within bounds
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

                // Slide images according to the slide button clicks
                slideButtons.forEach((button) => {
                    button.addEventListener("click", () => {
                        const direction = button.id === "prev-slide" ? -1 : 1;
                        const scrollAmount = imageList.clientWidth * direction;
                        if (imageList) {
                            imageList.scrollBy({ left: scrollAmount, behavior: "smooth" });
                        }
                    });
                });

                // Show or hide slide buttons based on scroll position
                const handleSlideButtons = () => {
                    if (imageList) {
                        slideButtons[0].style.display = imageList.scrollLeft <= 0 ? "none" : "flex";
                        slideButtons[1].style.display = imageList.scrollLeft >= maxScrollLeft ? "none" : "flex";
                    }
                };

                // Update scrollbar thumb position based on image scroll
                const updateScrollThumbPosition = () => {
                    if (imageList) {
                        const scrollPosition = imageList.scrollLeft;
                        const thumbPosition = (scrollPosition / maxScrollLeft) * (sliderScrollbar.clientWidth - scrollbarThumb.offsetWidth);
                        scrollbarThumb.style.left = `${thumbPosition}px`;
                    }
                };

                // Call these two functions when image list scrolls
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
    
    return (
        <>
            <div className="containerr">
                <div className="slider-wrapper">
                    <button id="prev-slide" className="slide-button material-symbols-rounded">
                        chevron_left
                    </button>
                    <ul className="image-list">
                        <div className="image-item-container" id="image-item-container-1">
                            <img className="image-item" src={rs1} alt="img-1" />
                            <div className="image-overlay">
                            <p className="cnt">Lorem ipsum dolor sit amet1</p>
                            </div>
                        </div>
                        <div className="image-item-container" id="image-item-container-2">
                        <img className="image-item" src={rs2} alt="img-2" />
                            <div className="image-overlay">
                                <p className="cnt">Lorem ipsum dolor sit amet2</p>
                            </div>
                        </div>
                        <div className="image-item-container" id="image-item-container-3">
                        <img className="image-item" src={rs3} alt="img-3" />
                            <div className="image-overlay">
                            <p className="cnt">Lorem ipsum dolor sit amet3</p>
                            </div>
                        </div>
                        <div className="image-item-container" id="image-item-container-4">
                        <img className="image-item" src={rs4} alt="img-4" />
                            <div className="image-overlay">
                            <p className="cnt">Lorem ipsum dolor sit ame4t</p>
                            </div>
                        </div>
                        <div className="image-item-container" id="image-item-container-5">
                        <img className="image-item" src={rs5} alt="img-5" />
                            <div className="image-overlay">
                            <p className="cnt">Lorem ipsum dolor sit amet5</p>
                            </div>
                        </div>
                        <div className="image-item-container" id="image-item-container-6">
                        <img className="image-item" src={rs6} alt="img-6" />
                            <div className="image-overlay">
                            <p className="cnt">Lorem ipsum dolor sit amet6</p>
                            </div>
                        </div>
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
            </div>


        </>


    )



}

export default Plants;