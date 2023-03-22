window.addEventListener("load", Req)

function Req() {
    let category = document.getElementById("categorysel");
    let cate = category.options[category.selectedIndex].text;
    let country = document.getElementById("countrysel");
    let coun = country.options[country.selectedIndex].text;
    //////////////
    const proxyUrl = "https://cors-anywhere.herokuapp.com/"
    const url = `${proxyUrl}https://newsapi.org/v2/top-headlines?country=${coun}&category=${cate}&apiKey=dde8f08cdb94403581c0794b4abbe611`;
    const request = new Request(url);
    fetch(request)
        .then(response => response.json())
        .then((news) => {ShowInfo(news);})
        .catch(error => {console.log(error);});
}

function ShowInfo(data) {
    let root = document.getElementById("root");

    data.articles.forEach(elem => {
        console.log(elem);
        ///////////////// target="_blank"
        let newelem = document.createElement("div");
        newelem.style.width = "228px";
        newelem.style.margin = "10px";
        newelem.style.border = "solid 2px";
        ////
        let img = document.createElement("img");
        if (elem.urlToImage !== null) {
            img.setAttribute("src", elem.urlToImage);
        } else {
            img.setAttribute("src", "https://www.thermaxglobal.com/wp-content/uploads/2020/05/image-not-found.jpg");
        }
        img.style.marginTop = "5px";
        img.setAttribute("width", 200);
        img.setAttribute("height", 150);
        newelem.appendChild(img);
        //////////////////////////////////////////////
        let titlee = document.createElement("p");
        titlee.setAttribute("align", "center");
        titlee.innerHTML = elem.title;
        newelem.appendChild(titlee);
        //////////////////////////////////////////////
        let author = document.createElement("p");
        author.innerHTML = elem.author;
        newelem.appendChild(author);
        //////////////////////////////////////////////
        let lin = document.createElement("a");
        lin.setAttribute("target", "_blank");
        lin.setAttribute("href", elem.url);
        lin.innerHTML = "Link to original";
        newelem.appendChild(lin);
        ///
        root.appendChild(newelem);

    });
}

