// -----------------------------------------------------------------------------
//  <copyright file="AbstractMutablePhxCollectionTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    public abstract class AbstractMutablePhxCollectionTests : AbstractPhxCollectionsTestBase {
        protected abstract bool SupportsDuplicateItems { get; }

        [Test]
        public void AddUniqueItemToCollection() {
            var collection = Given("An empty mutable collection",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(new List<string>()));
            var itemToAdd = Given("An item not already in the list", () => "add me");

            var actual = When("The item is added to the collection", () => collection.Add(itemToAdd));

            Then("The expected result is returned",
                    true,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The item was added to the collection", () => Verify.That(collection.Contains(itemToAdd).IsTrue()));
        }

        [Test]
        public void AddDuplicateItemToCollection() {
            var elements = Given("A collection of elements", () => CreateElements(3));
            var collection = Given("An collection constructed with those elements.",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var itemToAdd = Given("An item already in the collection", () => "1");

            var actual = When("The item is added to the collection", () => collection.Add(itemToAdd));

            Then("The expected result is returned",
                    SupportsDuplicateItems,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The item was added to the collection", () => Verify.That(collection.Contains(itemToAdd).IsTrue()));
        }

        [Test]
        public void AddAllUniqueItemsToCollection() {
            const int numElementsToAdd = 3;
            var collection = Given("An empty mutable collection",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(new List<string>()));
            var itemsToAdd = Given("Items not already in the list", () => CreateElements(numElementsToAdd));

            var actual = When("The items are added to the collection", () => collection.AddAll(itemsToAdd));

            Then("The expected result is returned",
                    numElementsToAdd,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The item was added to the collection",
                    () => Verify.That(collection.ContainsAll(itemsToAdd).IsTrue()));
        }

        [Test]
        public void AddAllDuplicateItemToCollection() {
            const int numElementsToAdd = 3;
            var elements = Given("A collection of elements", () => CreateElements(3));
            var collection = Given("An collection constructed with those elements.",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var itemsToAdd = Given("Items already in the list", () => CreateElements(numElementsToAdd));

            var actual = When("The items are added to the collection", () => collection.AddAll(itemsToAdd));

            Then("The expected result is returned",
                    SupportsDuplicateItems
                            ? numElementsToAdd
                            : 0,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The item was added to the collection",
                    () => Verify.That(collection.ContainsAll(itemsToAdd).IsTrue()));
        }

        [Test]
        public void ClearRemovesAllItemsFromCollection() {
            var elements = Given("A collection of elements", () => CreateElements(3));
            var collection = Given("An collection constructed with those elements.",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));

            When("The collection is cleared", () => collection.Clear());

            Then("The collection is now empty", () => Verify.That(collection.IsEmpty().IsTrue()));
        }

        [TestCase(5, 3, true)]
        [TestCase(5, 7, false)]
        public void RemoveReturnsExpectedValue(int numElements, int elementToRemove, bool shouldBeRemoved) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                    "A collection constructed with those elements.",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var itemToAdd = Given("An item to remove from the collection", () => elementToRemove.ToString());

            var actual = When("The item is removed from the collection", () => collection.Remove(itemToAdd));

            Then("The expected result is returned",
                    shouldBeRemoved,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The item was removed from the collection",
                    () => Verify.That(collection.Contains(itemToAdd).IsFalse()));
        }

        [TestCase(5, 3, 2)]
        [TestCase(5, 6, 0)]
        public void RemoveAllWithPredicateReturnsExpectedValue(
                int numElements,
                int minRemoveElement,
                int numThatShouldBeRemoved
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                    "A collection constructed with those elements",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var min = Given("A min value to remove", () => minRemoveElement.ToString());

            var actual = When("The items greater than the min are removed from the collection",
                    () => collection.RemoveAll((item) => item.CompareTo(min) >= 0));

            Then("The expected result is returned",
                    numThatShouldBeRemoved,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The items were removed from the collection",
                    () => Verify.That(collection.ContainsAny((item) => item.CompareTo(min) >= 0).IsFalse()));
        }

        [TestCase(5, 0, 3, 3)]
        [TestCase(5, 1, 1, 1)]
        [TestCase(5, 2, 5, 3)]
        [TestCase(5, 6, 3, 0)]
        public void RemoveAllReturnsExpectedValue(
                int numElements,
                int minRemoveElement,
                int numRemoveElements,
                int numThatShouldBeRemoved
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                    "A collection constructed with those elements",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var itemsToRemove = Given("A collection of items to remove",
                    () => CreateElements(numRemoveElements, minRemoveElement));

            var actual = When("The items are removed from the collection", () => collection.RemoveAll(itemsToRemove));

            Then("The expected result is returned",
                    numThatShouldBeRemoved,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The items were removed from the collection",
                    () => Verify.That(collection.ContainsAny(itemsToRemove).IsFalse()));
        }

        [TestCase(5, 3, 3)]
        [TestCase(5, 6, 5)]
        public void RetainOnlyWithPredicateReturnsExpectedValue(
                int numElements,
                int minRetainElement,
                int numThatShouldBeRemoved
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                    "A collection constructed with those elements",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var min = Given("A min value to retain", () => minRetainElement.ToString());

            var actual = When("The items greater than the min are retained in the collection",
                    () => collection.RetainOnly((item) => item.CompareTo(min) >= 0));

            Then("The expected result is returned",
                    numThatShouldBeRemoved,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The items were retained in the collection",
                    () => Verify.That(collection.All((item) => item.CompareTo(min) >= 0).IsTrue()));
        }

        [TestCase(5, 0, 3, 2)]
        [TestCase(5, 1, 1, 4)]
        [TestCase(5, 2, 5, 2)]
        [TestCase(5, 6, 3, 5)]
        public void RetainOnlyReturnsExpectedValue(
                int numElements,
                int minRetainElement,
                int numRetainElements,
                int numThatShouldBeRemoved
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                    "A collection constructed with those elements",
                    () => GetTestInstance<IPhxMutableCollection<string>, string>(elements));
            var itemsToRetain = Given("A collection of items to retain",
                    () => CreateElements(numRetainElements, minRetainElement));

            var actual = When("The items are retained in the collection", () => collection.RetainOnly(itemsToRetain));

            Then("The expected result is returned",
                    numThatShouldBeRemoved,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The items were retained in the collection",
                    itemsToRetain,
                    (expected) => Verify.That(collection.All(expected.Contains).IsTrue()));
        }
    }
}
