import '../assets/aboutus.css';
import rs from '../Images/cute-happy-funny-monstera-plant-pot-vector-cartoon-character-illustration-design-isolated_92289-1278.avif';
import r from '../Images/personal-growth.png'
function AboutUs() {
    return (
        <>
            <section className="hero" id="hero">
                <div className="heading">
                    <h1>Hakkimizda</h1>
                </div>
                <div className="container">
                    <div className="hero-content">
                        <h2>Web sitemize hos geldiniz</h2>
                        {/*<p>Bitkilerinizin t�r�n� ve sa�l���n� anlamak art�k daha kolay! Yapay zeka destekli web sitemiz, kullan�c�lar�n bitkilerinin yapraklar�ndan foto�raf alarak bitki t�r�n� belirlemelerine yard�mc� oluyor. Ayr�ca, bitkilerinizde olas� hastal�klar� tan�mlayarak detayl� bir analiz sunuyoruz. Kullan�c�lar, basit bir foto�raf y�kleyerek bitkilerinin ihtiya�lar�n� daha iyi anlayabilir ve olas� sorunlar� �nceden tespit edebilirler. Bitkilerinizi sa�l�kl� ve mutlu tutmak i�in �nerilerimizle do�ru bak�m� sa�lamak art�k �ok daha basit. Yapay zeka algoritmalar�m�z, geni� bitki veri taban�m�z� kullanarak h�zl� ve do�ru sonu�lar sunar. Bitkilerinizi sevgiyle b�y�tmek i�in bize kat�l�n ve do�an�n g�zelliklerini ke�fedin!</p>*/}
                        <p>Understanding the type and health of your plants is now easier! Our artificial intelligence-powered website helps users identify plant species by taking photos of their plants' leaves. We also provide a detailed analysis, identifying possible diseases in your plants. By uploading a simple photo, users can better understand their plant's needs and detect potential problems in advance. Providing proper care is now much simpler with our recommendations to keep your plants healthy and happy. Our artificial intelligence algorithms deliver fast and accurate results using our extensive plant database. Join us to grow your plants with love and discover the beauties of nature!</p>
                        <button className="cta-button">Hemen Basla</button>
                    </div>
                    <div className="hero-image">
                        <img src={rs} alt=""></img>
                    </div>
                </div>
            </section>
            <section>
                <div className="con">
                    <div className="head">
                        <h1>Our Team</h1>
                    </div>
                    <div className="sub-container">
                    <div className="teams">
                        <img src={r} alt=""></img>
                        <div className="name">Eda Dilek</div>
                        <div className="desig">Developer</div>
                        <div className="about">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ornare congue nulla in cursus. In ut elit in nulla gravida efficitur et eget diam. Fusce et tortor dapibus, iaculis nibh eu, lobortis sem. Fusce tincidunt mauris in tellus volutpat egestas.</div>
                        <div className="social-links">
                            <a href="#"><i className="fab fa-facebook"></i></a>
                            <a href="#"><i className="fab fa-instagram"></i></a>
                            <a href="#"><i className="fab fa-twitter"></i></a>
                            <a href="#"><i className="fab fa-github"></i></a>
                        </div> 
                    </div>
                    <div className="teams">
                        <img src={r} alt=""></img>
                        <div className="name">Mahmut Enes Cevik</div>
                        <div className="desig">Developer</div>
                        <div className="about">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ornare congue nulla in cursus. In ut elit in nulla gravida efficitur et eget diam. Fusce et tortor dapibus, iaculis nibh eu, lobortis sem. Fusce tincidunt mauris in tellus volutpat egestas.</div>
                        <div className="social-links">
                            <a href="#"><i className="fab fa-facebook"></i></a>
                            <a href="#"><i className="fab fa-instagram"></i></a>
                            <a href="#"><i className="fab fa-twitter"></i></a>
                            <a href="#"><i className="fab fa-github"></i></a>
                        </div>
                    </div>
                    <div className="teams">
                        <img src={r} alt=""></img>
                        <div className="name">Berke Erkan Dogan</div>
                        <div className="desig">Developer</div>
                        <div className="about">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ornare congue nulla in cursus. In ut elit in nulla gravida efficitur et eget diam. Fusce et tortor dapibus, iaculis nibh eu, lobortis sem. Fusce tincidunt mauris in tellus volutpat egestas.</div>
                        <div className="social-links">
                            <a href="#"><i className="fab fa-facebook"></i></a>
                            <a href="#"><i className="fab fa-instagram"></i></a>
                            <a href="#"><i className="fab fa-twitter"></i></a>
                            <a href="#"><i className="fab fa-github"></i></a>
                        </div>
                    </div>
                    <div className="teams">
                        <img src={r} alt=""></img>
                        <div className="name">Ertan Soyalp</div>
                        <div className="desig">Developer</div>
                        <div className="about">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ornare congue nulla in cursus. In ut elit in nulla gravida efficitur et eget diam. Fusce et tortor dapibus, iaculis nibh eu, lobortis sem. Fusce tincidunt mauris in tellus volutpat egestas.</div>
                        <div className="social-links">
                            <a href="#"><i className="fab fa-facebook"></i></a>
                            <a href="#"><i className="fab fa-instagram"></i></a>
                            <a href="#"><i className="fab fa-twitter"></i></a>
                            <a href="#"><i className="fab fa-github"></i></a>
                        </div>
                    </div>
                    </div>
                </div>
            </section>


        </>


    )



}

export default AboutUs;
