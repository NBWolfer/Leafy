


function ErrorPage(message: string, code: string) {
    return (
        <div>
            <h1>{message}</h1>
            <h2>{code}</h2>
        </div>
    )

}

export default ErrorPage;