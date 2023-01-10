// Write your Javascript code.

/*<script> FOR SHOW ALL BOOKS BUTTON INDEX.CSHTML NOT IMPLEMENTAD*/
    $(document).ready(function () {
        showAllBooks();
        });

    function showAllBooks() {
        // Send a GET request to the server to retrieve all books
        $.get("/Home/AllBooks", function (data) {
            //clear current booklist
            $(".books .book-card").remove();
            //loop through returned books and add them to the view
            for (var i = 0; i < data.length; i++) {
                var book = data[i];
                var bookCard =
                    '<div class="book-card" onclick="window.location.href=\'/Home/BookDetail?bookId=' + book.Id + '\'">' +
                    '<div class="book-image">' +
                    '<img src="/Uploads/' + book.BookImage + '">' +
                    '</div>' +
                    '<div class="book-info">' +
                    '<h4>' + book.Title + '</h4>' +
                    '<h4>Author : ' + book.Author + '</h4>' +
                    '<h4>Genre: ' + book.GenreNames + '</h4>' +
                    '<h4>' + book.ReleaseYear + '</h4>' +
                    '</div>' +
                    '</div>';
                $(".books").append(bookCard);
            }
        });
        }
/*</script>*/
