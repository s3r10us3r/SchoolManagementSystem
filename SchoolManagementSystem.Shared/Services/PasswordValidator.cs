namespace SchoolManagementSystem.Shared.Services
{
    public class PasswordValidator
    {
        public bool IsPasswordValid(string password, out string message)
        {
            if (!IsPasswordLongEnough(password))
            {
                message = "Password is too short.";
                return false;
            }
            if (!DoesPasswordContainAllCharacterSets(password))
            {
                message = "Password must contain an upper and lower case letter";
                return false;
            }
            message = "";
            return true;
        }

        private bool IsPasswordLongEnough(string password)
            => password.Length >= 10;

        private bool DoesPasswordContainAllCharacterSets(string password)
        {
            var hasLowerLetter = password.Any(char.IsLower);
            var hasUpperLetter = password.Any(char.IsUpper);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecialCharacter = password.Any(c => !char.IsLetterOrDigit(c));
            return hasLowerLetter && hasUpperLetter && hasDigit && hasSpecialCharacter;
        }
    }
}
