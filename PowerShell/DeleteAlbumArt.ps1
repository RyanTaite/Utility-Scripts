# Specify the root directory of your music collection
# "C:\Path\To\Your\Music"
$musicRootDirectory = "C:\Users\rtaite\SynologyDrive\Music"

# Define an array of image file extensions
$imageExtensions = @(".jpg", ".jpeg", ".png", ".gif", ".bmp")

# Get all image files within the specified directory and its subdirectories
$imageFiles = Get-ChildItem -Path $musicRootDirectory -Recurse -File |
    Where-Object { $imageExtensions -contains $_.Extension.ToLower() }

# Check if any image files were found
if ($imageFiles.Count -eq 0) {
    Write-Host "No image files found in the specified directory and its subdirectories."
} else {
    # Display the list of image files
    Write-Host "Image files found:"
    $imageFiles | ForEach-Object { Write-Host $_.FullName }

    # Prompt the user to delete the image files
    $deleteImages = Read-Host "Do you want to delete these image files? (Y/N)"

    if ($deleteImages -eq 'Y' -or $deleteImages -eq 'y') {
        # Delete the image files
        $imageFiles | ForEach-Object {
            Remove-Item $_.FullName -Force
            Write-Host ("Deleted: " + $_.FullName)
        }
        Write-Host "All image files have been deleted."
    } else {
        Write-Host "Operation canceled. No files were deleted."
    }
}
