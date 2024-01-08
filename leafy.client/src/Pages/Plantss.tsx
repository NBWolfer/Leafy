import rs1 from '../Images/2119927025-0.png'
import rs2 from '../Images/calendula.png'
import rs3 from '../Images/4025565.jpg'
import rs4 from '../Images/623d22d72290ed75f7c45c8ca902f7ba.jpg'
import rs5 from '../Images/icons8-close-100.png'
import rs6 from '../Images/personal-growth.png'
/*import "https://fonts.googleapis.com/css2?family=Materiaal+Symbols+Rounded:opsz,wght,FILL,GRAD@48,400,0,0"*/
import '../assets/plants.css'
import '../assets/plantss.js'



function Plants() {
    
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