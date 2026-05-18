using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Shared.Logging
{
    public static class LogMessages
    {
        #region User Authentication

        public static string LoginUserSuccess(UserEntity userEntity) =>
            $"User logged in successfully. User name: {userEntity.Name}";

        public static string TokenGenerateSuccess() =>
            "Token generated successfully.";

        public static string InvalidLoginInputs() =>
            "User login failed. Invalid email or password.";

        public static string MissingLoginCredentials() =>
            "Email and password are required.";

        #endregion

        #region User Validation

        public static string InvalidUserInputs() =>
            "Invalid user data.";

        #endregion

        #region User Not Found

        public static string CannotPerformActionOnUser(string action, string userId) =>
            $"Cannot {action} user. User with id {userId} was not found.";

        #endregion

        #region User CRUD

        public static string AddUserError(Exception ex) =>
            $"Error adding user. Details: {ex.Message}";

        public static string AddUserSuccess(UserEntity userEntity) =>
            $"User name: {userEntity.Name} - id: {userEntity.Id} added successfully.";

        public static string UpdateUserError(Exception ex) =>
            $"Error updating user. Details: {ex.Message}";

        public static string UpdateUserSuccess(UserEntity userEntity) =>
            $"User name: {userEntity.Name} - id: {userEntity.Id} updated successfully.";

        public static string DeleteUserError(Exception ex) =>
            $"Error deleting user. Details: {ex.Message}";

        public static string DeleteUserSuccess(UserEntity userEntity) =>
            $"User name: {userEntity.Name} - id: {userEntity.Id} deleted successfully.";

        public static string GetAllUsersError(Exception ex) =>
            $"Error retrieving users list. Details: {ex.Message}";

        public static string GetAllUsersSuccess() =>
            "Users retrieved successfully.";

        public static string GetUserByIdError(Exception ex) =>
            $"Error retrieving user by id. Details: {ex.Message}";

        public static string GetUserByIdSuccess(UserEntity userEntity) =>
            $"User name: {userEntity.Name} - id: {userEntity.Id} retrieved successfully.";

        #endregion

        #region Password

        public static string InvalidPassword() =>
            "Incorrect current password.";

        public static string UpdatePasswordSuccess() =>
            "Password updated successfully.";

        #endregion

        #region Book CRUD

        public static string AddBookError(Exception ex) =>
            $"Error adding book. Details: {ex.Message}";

        public static string AddBookSuccess(BookEntity bookEntity) =>
            $"Book title: {bookEntity.Title} - id: {bookEntity.Id} added successfully.";

        public static string UpdateBookError(Exception ex) =>
            $"Error updating book. Details: {ex.Message}";

        public static string UpdateBookSuccess(BookEntity bookEntity) =>
            $"Book title: {bookEntity.Title} - id: {bookEntity.Id} updated successfully.";

        public static string DeleteBookError(Exception ex) =>
            $"Error deleting book. Details: {ex.Message}";

        public static string DeleteBookSuccess(BookEntity bookEntity) =>
            $"Book title: {bookEntity.Title} - id: {bookEntity.Id} deleted successfully.";

        public static string GetAllBooksError(Exception ex) =>
            $"Error retrieving books list. Details: {ex.Message}";

        public static string GetAllBooksSuccess() =>
            "Books retrieved successfully.";

        public static string GetBookByIdError(Exception ex) =>
            $"Error retrieving book by id. Details: {ex.Message}";

        public static string GetBookByIdSuccess(BookEntity bookEntity) =>
            $"Book title: {bookEntity.Title} - id: {bookEntity.Id} retrieved successfully.";

        public static string BookAlreadyExistsError(string title, string author) =>
            $"A book with title '{title}' and author '{author}' already exists and cannot be added.";

        public static string BookIsbnAlreadyExistsError(string isbn) =>
            $"A book with ISBN '{isbn}' already exists and cannot be added.";

        public static string BookAlreadyActiveError(string title) =>
            $"A book with title '{title}' is already active.";

        #endregion
    }
}