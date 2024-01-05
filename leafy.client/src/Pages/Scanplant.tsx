import React from "react";
import ImageUploading, { ImageListType } from "react-images-uploading";

export function Scanplant() {
    const [images, setImages] = React.useState([]);
    const maxNumber = 69;

    const onChange = (
        imageList: ImageListType,
        addUpdateIndex: number[] | undefined
    ) => {
        // data for submit
        console.log(imageList, addUpdateIndex);
        setImages(imageList as never[]);
    };

    return ( 
        <div className="App">
            <ImageUploading
                multiple
                value={images}
                onChange={onChange}
                maxNumber={maxNumber}
            >
                {({
                    imageList,
                    onImageUpload,
                    onImageRemoveAll,
                    onImageUpdate,
                    onImageRemove,
                    isDragging,
                    dragProps
                }) => (
                    // write your building UI
                    <div className="upload__image-wrapper">
                        <button
                            style={isDragging ? { color: "red" } : undefined}
                            onClick={onImageUpload}
                            {...dragProps}
                        >
                            Click or Drop here
                        </button>
                        &nbsp;
                        <button onClick={onImageRemoveAll}>Remove all images</button>
                        {imageList.map((image, index) => (
                            <div key={index} className="image-item">
                                <img src={image.dataURL} alt="" width="100" />
                                <div className="image-item__btn-wrapper">
                                    <button onClick={() => onImageUpdate(index)}>Update</button>
                                    <button onClick={() => onImageRemove(index)}>Remove</button>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </ImageUploading>
        </div>
    );
}
export default Scanplant;


//import axios from 'axios'
//import { useState } from 'react'



//function Scanplant() {
//    const [image, setImage] = useState('')
//    function handleImage(e) {
//        console.log(e.target.files)
//        setImage(e.target.files[0])

//    }

//    function handleApi() {
//        const formData = new FormData()
//        formData.append('image', image)
//        axios.post('url', formData).then((res) => {
//            console.log(res)
//        })

//    }
//    return (
//        <>
//            <div>
//                <input type="file" name="file" onChange={handleImage} />
//                <button onClick={handleApi}>Submit</button>
//            </div>
//        </>

//    )
//}
//export default Scanplant;

/*---base64çevirme*/
//const imageList = [
//    { dataURL: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA...' },
//    // Diðer resim nesneleri
//];

//const base64StringArray = imageList.map((image) => {
//    // 'data:image/png;base64,' kýsmýný kaldýrarak sadece base64 veriyi alýn
//    const base64Data = image.dataURL.split(',')[1];

//    // base64 veriyi stringe çevirin
//    const binaryData = atob(base64Data);

//    return binaryData;
//});

//console.log(base64StringArray);