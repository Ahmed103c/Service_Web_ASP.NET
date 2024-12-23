namespace PlateformeFilm.Models{
    public class Film{
        public int Id{get;set;}
        public string Title{get;set;}

        public string Poster{get;set;}

        public string IMDB{get;set;}

        public int dateDeSortie{get;set;}
        public Film(){}
    }
}