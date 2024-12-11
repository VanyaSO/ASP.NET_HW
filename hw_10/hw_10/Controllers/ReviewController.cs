using hw_10.Models;
using hw_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace hw_10.Controllers;

public class ReviewController : Controller
{
    private readonly ReviewService _reviews;
    public ReviewController(ReviewService reviews)
    {
        _reviews = reviews;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddReview(Review review)
    {
        if (ModelState.IsValid)
        {
            await _reviews.AddReviewAsync(review);
            return RedirectToAction(nameof(Book), "Book", new {bookId = review.BookId});   
        }

        return RedirectToAction(nameof(Book), "Book");
    }
}