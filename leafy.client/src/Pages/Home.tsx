/*import PlantList from "../Components/PlantTable";*/
import '../assets/home.css';
import rsm from '../Images/4025565.jpg'; 

function Home() {
    return (
    <>
    <section className="home" id="home">
        <div className="content">
            <h3>kesfetmeye basla</h3>
            <p>Bitkinin fotografini yukle gerisini yapay zekaya birak</p>
            <a href="#" className="btn">basla</a>
        </div>  
            </section>

    <section className="about" id="about">
        <h1 className="heading"> leafy</h1>
        <div className="row">
            <div className="image">
                    <img src={rsm} alt=""></img>
            </div>
            <div className="content">
                <h3>Nasil Calisir? </h3>
                <p>Yazilimimiz sayesinde yuklediginiz fotograflar yapay zeka tarafindan analiz edilir.</p>
                <p>Tek yapmaniz gereken bitkinizin yapraginin fotografini net bir sekilde cekip siteye yuklemeniz.</p>
                <a href="#" className="btn">daha fazla</a>
            </div>
            
        </div>

    </section>
    </>


)



}

export default Home;