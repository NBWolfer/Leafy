import React, { ChangeEvent } from "react";

interface ImageUploadProps {
    onBase64DataChange: (base64Data: string | null) => void;
}

interface ImageUploadState {
    base64Data: string | null;
}

class ImageUpload extends React.Component<ImageUploadProps, ImageUploadState> {
    constructor(props: ImageUploadProps) {
        super(props);
        this.state = {
            base64Data: null
        };
    }

    onChange = (e: ChangeEvent<HTMLInputElement>) => {
        debugger;
        console.log("file uploaded: ", e.target.files?.[0]);
        let file = e.target.files?.[0];

        if (file) {
            const reader = new FileReader();
            reader.onload = this._handleReaderLoaded.bind(this);
            reader.readAsBinaryString(file);
        }
    };

    _handleReaderLoaded = (e: ProgressEvent<FileReader>) => {
        console.log("file uploaded 2: ", e);
        let binaryString = (e.target as FileReader).result as string;
        this.setState({
            base64Data: btoa(binaryString)
        });

        // Call the callback function with the base64Data
        this.props.onBase64DataChange(btoa(binaryString));
    };

    render() {
        const { base64Data } = this.state;
        return (
            <div>
                <input
                    type="file"
                    name="image"
                    id="file"
                    accept=".jpg, .jpeg, .png"
                    onChange={(e) => this.onChange(e)}
                />

                <br />
                {base64Data != null && <img src={`data:image;base64,${base64Data}`} alt="uploaded" />}
            </div>
        );
    }
}

export default ImageUpload;
